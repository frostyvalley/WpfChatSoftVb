Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Windows.Threading

Class MainWindow

    'Private ServerSocket As Socket
    Private ServerIP As String = "127.0.0.1"
    Private ServerAddress As IPAddress = IPAddress.Parse(ServerIP)
    Private ServerPort As String = "8099"
    Private ServerEndPoint As IPEndPoint

    Private isServerConnected = False

    Private ConnSocket As Socket

    Private ConnThread As Thread
    Private Buffer(2048) As Byte
    Private MsgReceived As String

    Private Sub BtnConnectServe_Click(sender As Object, e As RoutedEventArgs) Handles BtnConnectServe.Click
        ServerEndPoint = New IPEndPoint(ServerAddress, Integer.Parse(ServerPort))
        ConnSocket = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        Try
            ConnSocket.Connect(ServerEndPoint)
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
            'If ConnSocket.Receive(Buffer) = 0 Then

            'End If
            'ShowMsgDelegate(ConnSocket.Receive(Buffer))
            MsgReceived = Encoding.Default.GetString(Buffer, 0, ConnSocket.Receive(Buffer))
            ShowMsgDelegate("收到信息: " & MsgReceived)
        End While
    End Sub

    Private Delegate Sub MsgDelegate(ByVal msg As String)

    Private Sub ShowMsgDelegate(ByVal msg As String)
        Me.Dispatcher.BeginInvoke(DispatcherPriority.Normal, New MsgDelegate(AddressOf ShowMsg), msg)
    End Sub

    Private Sub ShowMsg(msg As String)
        TxtShow.AppendText(msg & vbCrLf)
        TxtShow.ScrollToEnd()
        ClearInputBox()
    End Sub

    Private Sub ClearInputBox()
        TxtInput.Text = vbNullString
        Keyboard.Focus(TxtInput)
    End Sub
End Class
