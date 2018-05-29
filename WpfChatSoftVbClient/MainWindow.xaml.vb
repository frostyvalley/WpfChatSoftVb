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
        ClientSocket.Connect(ServerEndPoint)

        ConnThread = New Thread(AddressOf ReceiveMsg) With {
            .IsBackground = True
        }
    End Sub

    Private Sub ReceiveMsg()

    End Sub
End Class
