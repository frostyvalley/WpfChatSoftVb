Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Windows.Threading

Class MainWindow

    Private ServerSocket As Socket
    Private ServerIP As String = "127.0.0.1"
    Private ServerPort As String = "8099"
    Private ServerAddress As IPAddress = IPAddress.Parse(ServerIP)
    Private ServerEndPoint As IPEndPoint
    Private Backlog As Integer = 0

    Private ConnSocket As Socket
    Private ConnThread As Thread

    Private ClientEndPoint As IPEndPoint
    Private ClientAddress As IPAddress
    Private ClientPort As String

    Private IsServerStarted As Boolean = False
    Private isClientConnected As Boolean = False

    Public Sub New()
        StartClient()
    End Sub

    Private Sub StartClient()
        Dim ClientWindoww As New WpfChatSoftVbClient.MainWindow
        ClientWindoww.Show()
    End Sub

    Private Sub BtnStartServer_Click(sender As Object, e As RoutedEventArgs) Handles BtnStartServer.Click
        If ServerSocket Is Nothing Then
            ServerEndPoint = New IPEndPoint(ServerAddress, Integer.Parse(ServerPort))
            ServerSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            ServerSocket.Bind(ServerEndPoint)
        End If
        ServerSocket.Listen(Backlog)
        IsServerStarted = True
        ShowMsg("服务器启动")
        ShowMsg("Server Addr: " & ServerEndPoint.ToString)

        ConnThread = New Thread(AddressOf Connect) With {
            .IsBackground = True
        }
        ConnThread.Start()

        Keyboard.Focus(TxtInput)
    End Sub

    Private Sub Connect()
        While IsServerStarted
            ConnSocket = ServerSocket.Accept
            isClientConnected = True
            ClientEndPoint = ConnSocket.RemoteEndPoint
            ClientAddress = ClientEndPoint.Address
            ClientPort = ClientEndPoint.Port.ToString

            ShowMsgDelegate("客户端连接")
            ShowMsgDelegate("Client Addr: " & ClientEndPoint.ToString)

            ConnSocket.Send(Encoding.UTF8.GetBytes("与服务器连接成功..."))

            ShowMsgDelegate("客户端断开连接")
        End While
    End Sub

    Private Sub BtnStopServer_Click(sender As Object, e As RoutedEventArgs) Handles BtnStopServer.Click
        If ConnSocket IsNot Nothing Then
            ConnSocket.Close()
            ConnSocket = Nothing
        End If
        If ServerSocket IsNot Nothing Then
            ConnThread.Abort()
            ConnThread = Nothing
            ServerSocket.Close()
            IsServerStarted = False
            ServerSocket = Nothing
        End If
        ShowMsg("服务器关闭")
    End Sub

    Private Sub BtnSendMsg_Click(sender As Object, e As RoutedEventArgs) Handles BtnSendMsg.Click
        If TxtInput.Text.Trim.Length <> 0 Then
            ShowMsg(TxtInput.Text & vbCrLf & "字符数: " & TxtInput.Text.Length.ToString)
        End If
        ClearInputBox()
    End Sub

    Private Sub TxtInput_KeyDown(sender As Object, e As KeyEventArgs) Handles TxtInput.KeyDown
        If e.Key = Key.Enter Then
            If TxtInput.Text.Trim.Length <> 0 Then
                ShowMsg(TxtInput.Text & vbCrLf & "字符数: " & TxtInput.Text.Length.ToString)
            End If
            ClearInputBox()
        End If
    End Sub

    'Delegate Sub MsgDelegate(ByVal msg As String)

    'Private Sub DlgShowMsg(ByVal msg As String)
    '    Dim MsgDlg As New MsgDelegate(AddressOf ShowMsg)
    '    MsgDlg.Invoke("aha")
    'End Sub

    Private Delegate Sub MsgDelegate(ByVal msg As String)

    Private Sub ShowMsgDelegate(ByVal msg As String)
        Me.Dispatcher.BeginInvoke(DispatcherPriority.Normal, New MsgDelegate(AddressOf ShowMsg), msg)
    End Sub

    Private Sub ShowMsg(ByVal msg As String)
        TxtShow.AppendText(msg & vbCrLf)
        TxtShow.ScrollToEnd()
    End Sub

    Private Sub ClearInputBox()
        TxtInput.Text = vbNullString
        Keyboard.Focus(TxtInput)
    End Sub
End Class
