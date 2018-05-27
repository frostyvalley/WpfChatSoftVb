﻿Imports System.Net
Imports System.Net.Sockets
Imports System.Threading
Imports System.Threading.Thread

Class MainWindow

    Private ServerSocket As Socket
    Private ServerIP As String = "127.0.0.1"
    Private ServerPort As String = "8099"
    Private ServerAddress As IPAddress
    Private ServerEndPoint As IPEndPoint

    Private Backlog As Integer = 2

    Private ConnSocket As Socket
    Private ConnThread As Thread

    Private IsServerStarted As Boolean = False

    Public Sub New()
        StartClient()
    End Sub

    Private Sub StartClient()
        'Dim ClientWindoww As New WpfChatSoftVbClient.MainWindow
        'ClientWindoww.Show()
    End Sub

    Private Sub BtnStartServer_Click(sender As Object, e As RoutedEventArgs) Handles BtnStartServer.Click
        If ServerSocket Is Nothing Then
            ServerAddress = IPAddress.Parse(ServerIP)
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
        'ConnThread.Start()
    End Sub

    Private Sub Connect()
        While IsServerStarted.Equals(True)
            DlgShowMsg("启动测试...")
            IsServerStarted = False
        End While
    End Sub

    Private Sub BtnSendMsg_Click(sender As Object, e As RoutedEventArgs) Handles BtnSendMsg.Click
        If TxtInput.Text.Length <> 0 Then
            ShowMsg(TxtInput.Text & vbCrLf & "字符数: " & TxtInput.Text.Length.ToString)
        End If
        ClearInputBox()
    End Sub

    Delegate Sub MsgDelegate(ByVal msg As String)

    Private Sub DlgShowMsg(ByVal msg As String)
        Dim MsgDlg As New MsgDelegate(AddressOf ShowMsg)
        MsgDlg.Invoke("aha")
    End Sub

    Private Sub ShowMsg(ByVal msg As String)
        TxtShow.AppendText(msg & vbCrLf)
    End Sub

    Private Sub ClearInputBox()
        TxtInput.Text = vbNullString
    End Sub
End Class
