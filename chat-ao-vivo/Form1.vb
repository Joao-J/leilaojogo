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
        Dim linhadecomando = "server=localhost;user=root;database=comentarios;port=3306;password=140820"
        Try
            conecta = New MySqlConnection(linhadecomando)
            conecta.Open()
            MsgBox("conectado", vbOKOnly, "Banco de Dados ʙʏ ᴛᴇɴᴀᴛᴀ")
        Catch ex As Exception
        End Try
    End Sub

    Public Sub manobra()

        Dim criterio2 As String = ""
        Dim criterio As String = ""
        Dim tabela As New Data.DataTable
        Dim comandos As MySqlCommand
        Dim nome As String = "bomba"


        Try
            Dim query As String = "SELECT COUNT(*) FROM nome_comentario"
            Dim temDados As Boolean = False
            Using command As New MySqlCommand(query, conecta)
                Try

                    'Dim count As Integer = Convert.ToInt32(command.ExecuteScalar())

                    'If count <= 0 Then

                    comandos = New MySqlCommand("insert into nome_comentario(nome) values(@nome);", conecta)
                    With comandos
                        '.Parameters.Add("@id", MySqlDbType.Int32).Value = 1
                        .Parameters.Add("@nome", MySqlDbType.VarChar).Value = nome
                    End With
                    ''MsgBox("passou", vbOKOnly)
                    comandos.ExecuteNonQuery()
                    comandos.Parameters.Clear()

                    'End If
                Catch ex As Exception

                    MessageBox.Show("Erro ao verificar dados no banco de dados: " & ex.Message)
                End Try
            End Using
        Catch

        End Try
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
                                    Me.Controls.Add(c.criar())
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
            ElseIf Me.Controls.Contains(i.Value.acessar_c) Then
                i.Value.remove_player()
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
                    TryCast(c, TransparentPictureBox).Left += i.Value.move(c.Left)
                    TryCast(c, TransparentPictureBox).Top += i.Value.gravity(c.Top)
                End If
            Catch
            End Try
        Next
    End Sub


    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        For Each i In dic
            If Not i.Key.Contains("Principais mensagens") And i.Value.tempo > 0 Then
                i.Value.mudar_spr()
            End If
        Next
    End Sub

    Private Sub Timer6_Tick(sender As Object, e As EventArgs) Handles Timer6.Tick
        WebView21.CoreWebView2.Reload()
    End Sub
End Class

Public Class incrito
    Public tempo As Integer = 0
    Public msg As String
    Public nome_player As String
    Dim r As Random = New Random
    Dim n_player As Integer = 0
    Dim local As Integer = 0
    Dim p As New TransparentPictureBox
    Dim l As New Label
    Dim p_msg As New Label
    Dim c As New Panel
    Dim direita As New List(Of Image)
    Dim esqueda As New List(Of Image)
    Dim move_p As Integer = 0
    Dim dp As Integer = 0
    Dim de As Integer = 0
    Dim controle As Boolean = False
    Dim pulo As Integer = 0

    Private Sub addimages()
        direita.Add(My.Resources.d1)
        direita.Add(My.Resources.d2)
        direita.Add(My.Resources.d3)
        direita.Add(My.Resources.d4)
        esqueda.Add(My.Resources.e1)
        esqueda.Add(My.Resources.e2)
        esqueda.Add(My.Resources.e3)
        esqueda.Add(My.Resources.e4)
    End Sub
    Public Sub tratamento_msg()
        Dim synth As New SpeechSynthesizer
        With msg.ToLower()
            Dim altmsg As String = msg.ToLower
            If .Contains("*controle") Then
                If controle = True Then
                    controle = False
                    altmsg = altmsg.Replace("*controle", "controle desabilitado")
                Else
                    local = p.Left
                    controle = True
                    altmsg = altmsg.Replace("*controle", "controle habilitado")
                End If

            ElseIf .Contains("*dir") And controle = True Then
                local += 30
                altmsg = altmsg.Replace("*dir", "")
            ElseIf .Contains("*esq") And controle = True Then
                local -= 30
                altmsg = altmsg.Replace("*esq", "")
            ElseIf (.Contains("*pulo") Or .Contains("*jump")) And controle = True Then
                pulo = -30
                altmsg = altmsg.Replace("*pulo", "")
                altmsg = altmsg.Replace("*jump", "")
            ElseIf .Contains("*visivel") Then
                p.Visible = True
                altmsg = altmsg.Replace("*visivel", "")
            ElseIf .Contains("*invisivel") Then
                p.Visible = False
                altmsg = altmsg.Replace("*invisivel", "")
            ElseIf .Contains("*kiss") Or .Contains("*beijo") Then
                altmsg = altmsg.Replace("*beijo", "")
                altmsg = altmsg.Replace("*kiss", "")
                altmsg = beijo(p, altmsg)
            Else
                synth.Volume = 100
                synth.SelectVoiceByHints(VoiceGender.Female)
                synth.SpeakAsync(p.Name & " disse")
                Thread.Sleep(200)
            End If
            msg = altmsg
            If msg.ToLower.Contains("tenata") Then
                Form1.manobra()
            End If
            synth.Volume = 100
            synth.SelectVoiceByHints(VoiceGender.Female)
            synth.SpeakAsync(msg)
        End With
    End Sub
    Public Sub readd()
        Form1.Controls.Add(p)
        Form1.Controls.Add(l)
        Form1.Controls.Add(p_msg)
        tratamento_msg()
    End Sub
    Public Sub remove_player()
        Try
            With Form1.Controls
                .Remove(p)
                .Remove(l)
                .Remove(p_msg)
            End With
        Catch ex As Exception

        End Try
    End Sub
    Public Function beijo(p As Control, altmsg As String)
        Dim b As Boolean = False
        For Each outro_jogador As Control In Form1.Controls
            Try
                If outro_jogador.Tag = "aiai" And p.Name <> outro_jogador.Name Then
                    If outro_jogador.Bounds.IntersectsWith(p.Bounds) Then
                        altmsg = altmsg & vbNewLine & p.Name & " BEIJOU " & outro_jogador.Name & " <3"
                        If p.Left < outro_jogador.Left Then
                            p.BackgroundImage = My.Resources.e1
                            TryCast(outro_jogador, PictureBox).BackgroundImage = My.Resources.d1
                        Else
                            p.BackgroundImage = My.Resources.d1
                            TryCast(outro_jogador, PictureBox).BackgroundImage = My.Resources.e1
                        End If
                        b = True
                        Exit For
                    End If
                End If
            Catch
            End Try
        Next
        If b = False Then
            altmsg = altmsg & "não tem ninguem perto!"
        End If
        Return altmsg
    End Function

    Public Function criar()
        addimages()
        l.Text = nome_player
        l.TextAlign = ContentAlignment.MiddleCenter
        l.ForeColor = Color.Black
        l.Tag = nome_player & "nomekkk"
        l.Top = p.Top
        l.Left = r.Next(0, Form1.Width - l.Width)
        l.AutoSize = True
        l.BackColor = Color.Yellow
        p_msg.Font = New Font("arial", 12, FontStyle.Regular)
        p_msg.AutoSize = True
        p_msg.BackColor = Color.LimeGreen
        p_msg.ForeColor = Color.Black
        p_msg.TextAlign = ContentAlignment.TopCenter
        p_msg.Tag = nome_player & "*msg*"
        p.BackColor = Color.Transparent
        p.SizeMode = PictureBoxSizeMode.StretchImage
        p.BackgroundImage = My.Resources.parado
        p.BackgroundImageLayout = ImageLayout.Stretch
        p.Width = 50
        p.Name = nome_player
        p.Height = p.Width
        p.Top = Form1.Height - 100
        p.BackColor = Color.Transparent
        p.Left = c.Width / 2 - p.Width / 2
        p.Tag = "aiai"
        Form1.Controls.Add(l)
        Form1.Controls.Add(p_msg)
        Form1.AllowTransparency = True
        Form1.TransparencyKey = Color.Transparent
        tratamento_msg()
        Return (p)
    End Function

    Public Function gravity(y)
        Dim z As Integer = 0
        If pulo < 0 Then
            z = -1
            pulo += 1
        Else
            If y + p.Height + 50 < Form1.Height Then
                z = 1
            End If
        End If
        Return z
    End Function
    Public Function acessar_c()
        Return p
    End Function

    Private Sub move_nome()
        l.Top = p.Top - l.Height
        l.Left = p.Left + (p.Width / 2) - (l.Width / 2)
        p_msg.Top = l.Top - (p_msg.Height) - l.Height
        p_msg.Left = p.Left + (p.Width / 2) - (p_msg.Width / 2)
    End Sub

    Public Sub sendmsg(x As String)
        p_msg.Text = x
    End Sub

    Public Sub mudar_spr()

        If p.Left > local Then

            p.BackgroundImage = esqueda.Item(dp)
            If dp + 1 = esqueda.Count Then
                dp = 0
            Else
                dp += 1
            End If
        ElseIf p.Left < local Then
            p.BackgroundImage = direita.Item(de)
            If de + 1 = direita.Count Then
                de = 0
            Else
                de += 1
            End If
        Else
            p.BackgroundImage = My.Resources.parado
            de = 0
            dp = 0
        End If
    End Sub

    Public Function move(x)
        move_nome()
        Dim mexe As Integer = 1

        If x > local Then
            mexe = -1
        ElseIf x = local Then
            If controle = False Then
                local = r.Next(0, Form1.Width)
            End If
            mexe = 0
        End If
        Return mexe
    End Function

End Class