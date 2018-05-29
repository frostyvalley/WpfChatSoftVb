Imports System.Net
Imports System.Net.Sockets

Class MainWindow

    Private ServerSocket As Socket
    Private ServerIP As String = "127.0.0.1"
    Private ServerAddress As IPAddress = IPAddress.Parse(ServerIP)
    Private ServerPort As String = "8099"

    Private ClientSocket As Socket

    Private Sub BtnConnectServe_Click(sender As Object, e As RoutedEventArgs) Handles BtnConnectServe.Click
        ClientSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
    End Sub
End Class
