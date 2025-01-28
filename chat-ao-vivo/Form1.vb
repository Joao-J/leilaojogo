Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.Net
Imports System.Runtime.CompilerServices
Imports Microsoft.Web.WebView2.Core
Imports chat_ao_vivo.TransparentPictureBox
Imports System.Speech.Synthesis
Imports System.Threading
Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class Form1
    Dim url As String = "https://studio.youtube.com/live_chat?is_popout=1&v=T5PmZoaBEYg"
    Dim correndo As Boolean = False
    Dim comentarios As New List(Of String)
    Dim oqnp As New List(Of String)
    Dim dic As New Dictionary(Of String, incrito)
    Dim mensagens As String = ""

    Dim conecta As New MySqlConnection
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WebView21.Source = New Uri(url)
        Me.Width = 1300
        Me.Left = 0
        Me.Top = 0
        With oqnp
            .Add("Curta o chat ao vivo! Não se esqueça de proteger sua privacidade e seguir nossas diretrizes da comunidade.")
            .Add("Saiba mais")
            .Add("Todas as mensagens que você enviar serão exibidas ao público")
            .Add("Faça login para bater papo")
            .Add("null")
            .Add("Principais mensagens")
        End With
    End Sub

    Private Async Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If correndo = True Then
            downloadtexto()
        End If
    End Sub
    Dim nm As Integer = 0
    Dim p As Integer = 0
    Dim ultimo As String = ""
    Public Async Sub downloadtexto()
        Dim texto As String = Await WebView21.CoreWebView2.ExecuteScriptAsync("document.body.innerText")
        texto = texto.Replace("\n", vbNewLine).Replace("Principais mensagens", "Principais mensagens" & vbNewLine)
        Dim numero As Integer = texto.Split(vbNewLine).ToList().Count - 1
        Try
            Dim x = texto.Split(vbNewLine).ToList().GetRange(numero - 3, 2)
            Dim textom As String = x.Item(0).ToString & x.Item(1).ToString

            If ultimo <> textom Then
                ultimo = textom
                Dim k As String = ""
                If p = 0 Then
                    x.Clear()
                    p = 1
                End If

                Dim semcriatividade As Integer = 0
                For Each y As String In x

                    If Not oqnp.Contains(y) Then
                        If k = "" Then
                            k = y
                        Else
                            If dic.ContainsKey(k) = True Then
                                If semcriatividade + 2 > nm Then
                                    dic.Item(k).msg = y
                                    dic.Item(k).tempo = 300
                                    dic.Item(k).readd()
                                End If

                            ElseIf Not k.Contains("Principais mensa") Then
                                If k.Contains("Streamlabs") = True And Not y.Contains("Thanks for") = True Then
                                Else
                                    Dim c As New incrito
                                    c.msg = y
                                    c.tempo = 300
                                    c.nome_player = k
                                    dic.Add(k, c)
                                End If

                            End If
                            k = ""
                        End If
                    Else
                        k = ""
                    End If
                    semcriatividade += 1
                Next
                nm = semcriatividade
            End If
        Catch
        End Try
    End Sub

    Private Sub WebView21_CoreWebView2InitializationCompleted(sender As Object, e As Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs) Handles WebView21.CoreWebView2InitializationCompleted
        correndo = True
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        p = 0
        WebView21.Source = New Uri(TextBox2.Text)
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        mensagens = ""
        For Each i As KeyValuePair(Of String, incrito) In dic

            If Not i.Key.Contains("Principais mensagens") And i.Value.tempo > 0 Then
                If i.Value.msg.Replace(" ", "").Replace(vbNewLine, "") <> "" Then
                    i.Value.sendmsg(i.Value.msg & vbNewLine & "↓")
                End If
                mensagens = mensagens & i.Key & " >> " & i.Value.msg & " " & i.Value.tempo & vbNewLine
                Dim inc As incrito = i.Value
                inc.tempo -= 1
                dic.Item(i.Key) = inc


            End If
        Next

        TextBox1.Text = mensagens
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        For Each i As KeyValuePair(Of String, incrito) In dic
            Try
                If Not i.Key.Contains("Principais mensagens") And i.Value.tempo > 0 Then
                    Dim ind As Integer = Me.Controls.IndexOf(i.Value.acessar_c)
                    Dim c As Control = Me.Controls.Item(ind)
                End If
            Catch
            End Try
        Next
    End Sub


    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick

    End Sub

    Private Sub Timer6_Tick(sender As Object, e As EventArgs) Handles Timer6.Tick
        WebView21.CoreWebView2.Reload()
    End Sub
End Class

Public Class incrito
    Public tempo As Integer = 0
    Dim r As Random = New Random
    Public msg, nome_player As String

    Public Sub tratamento_msg()
        Dim synth As New SpeechSynthesizer
        With msg.ToLower()
            Dim altmsg As String = msg.ToLower

            msg = altmsg
            If msg.ToLower.Contains("tenata") Then
                Dim tempo As String = "oi"
            End If
            synth.Volume = 100
            synth.SelectVoiceByHints(VoiceGender.Female)
            synth.SpeakAsync(msg)
        End With
    End Sub
    Public Sub readd()
        tratamento_msg()
    End Sub

    Public Function acessar_c()
        Return p
    End Function

    Public Sub sendmsg(x As String)
        p_msg.Text = x
    End Sub





End Class