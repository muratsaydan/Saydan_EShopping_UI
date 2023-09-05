
Partial Class UyeIslemleri
    Inherits System.Web.UI.Page
	Dim SaydanClass As New SaydanClass
	Protected Sub lnkUyeCikis_Click(sender As Object, e As EventArgs) Handles lnkUyeCikis.Click
		Session("UserID") = Nothing
		Session("UserName") = Nothing
		Session("YoneticiMi") = Nothing
		Response.Redirect("index.aspx")
	End Sub

	Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
		SaydanClass.LoginKontroluYap()
		'If Session("UserID") Is Nothing Then Response.Redirect("login.aspx?x=sipdet2")
	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		If Not IsPostBack Then
			If SaydanClass.YoneticiMi Then
				pnlYonetici.Visible = True
			Else
				pnlYonetici.Visible = False
			End If
		End If
	End Sub
End Class
