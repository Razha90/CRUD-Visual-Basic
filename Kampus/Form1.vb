Imports System.Data.Odbc
Imports System.Reflection

Public Class Form1

    Dim Conn As OdbcConnection
    Dim Cmd As OdbcCommand
    Dim Ds As DataSet
    Dim Da As OdbcDataAdapter
    Dim Rd As OdbcDataReader
    Dim MyDB As String
    'Membuat Koneksi
    Sub Koneksi()
        ' Memanggil database yaitu nama database kita adalah db_universitas
        MyDB = "DSN=Mantap;"
        Conn = New OdbcConnection(MyDB)
        If Conn.State = ConnectionState.Closed Then Conn.Open()
    End Sub

    ' Memanggil Kondisi awal
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call KondisiAwal()
    End Sub

    ' Pertama kali dipanggil dengan membuat Sub KondisiAwal()
    Sub KondisiAwal()
        ' Textbox1, textbox2, textbox3, textbox4 kita kosongkan pertama kali
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        ' Textbox1, textbox2, textbox3, textbox4.enabled = false artinya formnya kita matikan
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        ' Textbox1 yang berarti form nim kita maksimalkan hanya bisa 15 huruf/angka
        TextBox1.MaxLength = 15
        ' Button1, Button2, Button3, Button4 kita tambahkan text masing -masing yaitu input, edit, hapus, tutup
        Button1.Text = "INPUT"
        Button2.Text = "EDIT"
        Button3.Text = "HAPUS"
        Button4.Text = "TUTUP"
        ' Button1, Button2, Button3, Button4 kita aktifkan dengan menggunakan perintah true
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        ' Panggil koneksi yang sudah kita buat sub Koneksi()
        Call Koneksi()
        ' Memanggil table yang sudah kita buat yaitu mahasiswa
        Da = New OdbcDataAdapter("Select * From mahasiswa", Conn)
        Ds = New DataSet
        Da.Fill(Ds, "mahasiswa")
        DataGridView1.DataSource = Ds.Tables("mahasiswa")
    End Sub

    Sub FieldAktif()
        ' untuk mengaktifkan form
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox1.Focus()
    End Sub
    ' Ini adalah proses simpan
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' if button1.text yang textnya "input" maka akan berubah menjadi button "simpan"
        If Button1.Text = "INPUT" Then
            Button1.Text = "SIMPAN"
            ' lalu button2 dan button3 tidak aktif dan button4 kita ubah menjadi tulisan "batal"
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Text = "BATAL"
            ' lalu kita panggil FieldAktif() yang mana textbox1,textbox2,textbox3,textbox4 akan diaktifkan
            Call FieldAktif()
        Else
            ' if textbox1, textbox2, textbox3 dan textbox3 kosong maka muncul alert pastikan semua field terisi
            ' ini artinya disebut validasi
            If TextBox1.Text = "" Or TextBox2.Text = "" Or
                TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Pastikan semua field terisi")
            Else
                ' Jika semua form terisi, maka kita panggil Koneksi() ke database
                Call Koneksi()
                ' lalu kita masukan data kita ke table mahasiswa (insert into mahasiswa .....)
                Dim InputData As String = "Insert into mahasiswa values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')"
                Cmd = New OdbcCommand(InputData, Conn)
                Cmd.ExecuteNonQuery()
                ' lalu kita tampilkan message "input data berhasil"
                MsgBox("Input data berhasil")
                ' lalu kita kembalikan ke KondisiAwal()
                Call KondisiAwal()
            End If
        End If
    End Sub
    ' Ini adalah proses EDIT
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'If Button2 edit kita klik maka dia akan berubah menjadi tulisan "update"
        If Button2.Text = "EDIT" Then
            Button2.Text = "UPDATE"
            'button1 dan button2 akan kita false artinya tidak berfungsi
            Button1.Enabled = False
            Button3.Enabled = False
            'button4 kita ganti tulisan menjadi batal
            Button4.Text = "BATAL"
            ' Lalu kita panggil FieldAktif() yang mana textbox1,TextBox2, TextBox3 dan textbox 4 kita aktifkan
            Call FieldAktif()
        Else
            ' ini adalah validasi jika textbox1, textbox2, TextBox3 dan textbox 4 tidak terisi maka akan muncul alert("pastikan semua terisi)
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Pastikan semua field terisi")
            Else
                ' jika semua terisi panggil Koneksi()
                Call Koneksi()
                ' kita update table mahasiswa
                Dim EditData As String = "Update mahasiswa set nama ='" & TextBox2.Text & "',alamat='" & TextBox3.Text & "',jurusan='" & TextBox4.Text & "'where nim='" & TextBox1.Text & "'"
                Cmd = New OdbcCommand(EditData, Conn)
                Cmd.ExecuteNonQuery()
                ' jika berhasil tampilkan alert / message ("edit Data berhasil")
                MsgBox("Edit data berhasil")
                ' setelah semua sudah tolong tampilkan
                KondisiAwal()
                Call KondisiAwal()
            End If
        End If
    End Sub

    ' Button3 adalah button hapus
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ' Jika button3.text yang mana textnya adalah hapus maka kita ubah texttnya menjadi delete
        If Button3.Text = "HAPUS" Then
            Button3.Text = "DELETE"
            ' button1 non aktifkan
            Button1.Enabled = False
            ' button2 juga kita non aktifkan, jadi dia ga bisa di klik sama sekali ketika button hapus kita klik
            Button2.Enabled = False
            ' button4.text kita ubah textnya dari tutup menjadi batal
            Button4.Text = "BATAL"
            ' setelah itu kita aktifkan FieldAktif() yang mana artinya kita mengaktifkan textbox1, textbox2, textbox3 dan TextBox4
            Call FieldAktif()
        Else
            ' Ini adalah validasi
            ' jika field tidak terisi maka tidak akan bisa di hapus
            If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
                MsgBox("Pastikan data yang akan dihapus terisi")
            Else
               ' jika sudah kita isi fieldnya maka bisa kita hapus, prosesnya adalah
               ' kita panggil Koneksi()
               Call Koneksi()
                ' lalu kita panggil table mahasiswa lalu dia bilang "tolongin aku dong, aku mau hapus dengan nim xxx tolong di bantu ya.makasih: )"
                Dim HapusData As String = "delete from mahasiswa where nim ='" & TextBox1.Text & "'"
                Cmd = New OdbcCommand(HapusData, Conn)
                Cmd.ExecuteNonQuery()
                ' kalau berhasil kita tampilkan alert / message dengan tulisan "hapus data berhasil"
                MsgBox("Hapus data berhasil")
            ' lalu kita panggil kondisiAwal()
            Call KondisiAwal()
        End If
        End If
    End Sub
    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        ' if chr 13 kita tekan, btw chr 13 itu adalah enter ya
        If e.KeyChar = Chr(13) Then
            ' setelah tekan entar maka Koneksi() akan terpanggil
            Call Koneksi()
            ' lalu ketika kita masukan nim di textbox1 (tekan enter)
            ' selanjutnya tolong panggilkan table mahasiswa dan tolong isi isi form textbox2 dan textbox3 dan textbox4
            Cmd = New OdbcCommand("Select * from mahasiswa where nim='" & TextBox1.Text & "'", Conn)
            Rd = Cmd.ExecuteReader
            Rd.Read()
            ' if kita panggil nim lalu tekan enter maka otomatis TextBox2, textbox3, dan textbox4 akan terisi secara otomatis
            If Rd.HasRows Then
                TextBox2.Text = Rd.Item("nama")
                TextBox3.Text = Rd.Item("alamat")
                TextBox4.Text = Rd.Item("jurusan")
            Else
                ' Jika nim yang kita ketikan salah, maka akan menampilkan alert atau message ("data tidak ada")
                MsgBox("Data Tidak Ada")
            End If
        End If
    End Sub

    ' ini adalah proses button4 yaitu tutup
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Jika button4 textnya TUTUP maka tolong end atau exit atau keluar dari aplikasi
        If Button4.Text = "TUTUP" Then
            End
            ' namun kalau tulisannya BATAL maka kita panggil
            KondisiAwal()
        Else
            Call KondisiAwal()
        End If
    End Sub
End Class