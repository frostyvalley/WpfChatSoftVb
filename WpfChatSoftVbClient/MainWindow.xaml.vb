Imports System.Net
Imports System.Net.Sockets
Imports System.Threading

Class MainWindow

    Private ServerSocket As Socket
    Private ServerIP As String = "127.0.0.1"
    Private ServerAddress As IPAddress = IPAddress.Parse(ServerIP)
    Private ServerPort As String = "8099"
    Private ServerEndPoint As IPEndPoint

    Private ClientSocket As Socket

    Private ConnThread As Thread

    Private Sub BtnConnectServe_Click(sender As Object, e As RoutedEventArgs) Handles BtnConnectServe.Click
        ServerEndPoint = New IPEndPoint(ServerAddress, Integer.Parse(ServerPort))
        ClientSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        Try
            ClientSocket.Connect(ServerEndPoint)
        Catch ex As Exception

        End Try


        ConnThread = New Thread(AddressOf ReceiveMsg) With {
            .IsBackground = True
        }
        ConnThread.Start()
    End Sub

    Private Sub ReceiveMsg()

    End Sub

    Private Sub ShowMsg(msg As String)
        TxtShow.AppendText(msg & "\n")
    End Sub
End Class
