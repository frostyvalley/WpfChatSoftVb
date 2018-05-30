Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading

Class MainWindow

    Private ServerSocket As Socket
    Private ServerIP As String = "127.0.0.1"
    Private ServerAddress As IPAddress = IPAddress.Parse(ServerIP)
    Private ServerPort As String = "8099"
    Private ServerEndPoint As IPEndPoint

    Private isServerConnected = False

    Private ClientSocket As Socket

    Private ConnThread As Thread
    Private Buffer(2048) As Byte

    Private Sub BtnConnectServe_Click(sender As Object, e As RoutedEventArgs) Handles BtnConnectServe.Click
        ServerEndPoint = New IPEndPoint(ServerAddress, Integer.Parse(ServerPort))
        ClientSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        Try
            ClientSocket.Connect(ServerEndPoint)
            ShowMsg("服务器连接成功")
            isServerConnected = True
        Catch ex As Exception
            ShowMsg("连接错误: 服务器丢失")
            Keyboard.Focus(BtnConnectServe)
        End Try

        ConnThread = New Thread(AddressOf ReceiveMsg) With {
            .IsBackground = True
        }
        ConnThread.Start()
    End Sub

    Private Sub ReceiveMsg()
        While isServerConnected
            Dim msg As String = Encoding.Default.GetString(Buffer)
        End While
    End Sub

    Private Delegate Sub MsgDelegate(ByVal msg As String)



    Private Sub ShowMsg(msg As String)
        TxtShow.AppendText(msg & vbCrLf)
        Keyboard.Focus(TxtInput)
    End Sub
End Class
