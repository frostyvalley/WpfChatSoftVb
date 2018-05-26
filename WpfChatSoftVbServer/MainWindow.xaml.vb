Imports System.Net
Imports System.Net.Sockets

Class MainWindow

    Private ServerSocket As Socket
    Private ServerIP As String = "127.0.0.1"
    Private ServerPort As String = "8009"
    Private ServerAddress As IPAddress
    Private ServerEndPoint As IPEndPoint

    Private ConnSocket As Socket

    Public Sub New()
        StartClient()
    End Sub

    Private Sub StartClient()
        Dim clientWindoww As New WpfChatSoftVbClient.MainWindow
        clientWindoww.Show()
    End Sub

    Private Sub BtnStartServer_Click(sender As Object, e As RoutedEventArgs) Handles BtnStartServer.Click

    End Sub

    Private Sub BtnSendMsg_Click(sender As Object, e As RoutedEventArgs) Handles BtnSendMsg.Click
        ServerAddress = IPAddress.Parse(ServerIP)
        ServerEndPoint = New IPEndPoint(ServerAddress, ServerPort)
        ShowMsg(ServerEndPoint.ToString)
    End Sub

    Private Sub ShowMsg(ByVal msg As String)
        TxtShow.AppendText(msg & vbCrLf)
    End Sub
End Class
