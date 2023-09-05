Imports System.Data

Partial Class Master2
	Inherits System.Web.UI.MasterPage

	Dim SaydanClass As New SaydanClass


	Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init

		If Not IsPostBack Then
			'Dim cmdstr As String = "select top 1 * from ISLEM_LOGLARI"
			'Dim dt As DataTable = SaydanClass._ConnDB(cmdstr)
			'Dim dr As DataRow = dt.NewRow
			'dr("Tarih") = Now
			'dr("SessionID") = Session("SessionID")
			'dr("IslemTipi") = "Sayfaya Giriş"
			'dr("IpAdresi") = Request.UserHostAddress
			'dr("SayfaAdi") = Me.Page.AppRelativeVirtualPath
			'dr("Parametre") = Me.Page.ClientQueryString
			'If Not Session("UserID") Is Nothing Then dr("MusteriKodu") = Session("UserID")
			'dt.Rows.Add(dr)
			'SaydanClass._SaveDB(cmdstr, dt)

		End If
	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

		'Dim whrcnd As String
		'If Not Session("UserID") = Nothing Then
		'	whrcnd = "SEPET.MusteriKodu = " + Session("UserID").ToString
		'	lblUserID.Text = Session("UserName")
		'	spnUye1.Visible = False
		'	spnUye2.Visible = True
		'Else
		'	whrcnd = "SEPET.SessionID = '" + Session("SessionID").ToString.ToString + "'"
		'	lblUserID.Text = "Misafir"
		'	spnUye1.Visible = True
		'	spnUye2.Visible = False
		'End If
		'Dim dtSepet As System.Data.DataTable = SaydanClass._ConnDB("select count(*) adet from (SELECT distinct [UrunKodu] FROM [SEPET] where " + whrcnd + " group by UrunKodu ) as A")
		'Dim SepettekiUrunKalemAdedi As Integer = dtSepet.Rows(0)(0)
		'dtSepet = SaydanClass._ConnDB("SELECT SUM(URUNLER.SatisFiyati * SEPET.Miktar) AS Tutar FROM SEPET INNER JOIN URUNLER ON SEPET.UrunKodu = URUNLER.StokKodu where " + whrcnd)
		'If IsDBNull(dtSepet.Rows(0)(0)) Then dtSepet.Rows(0)(0) = 0
		'Dim SepettekiUrunTutari As Integer = dtSepet.Rows(0)(0)
		'lblSepettekiUrunTutari.Text = SepettekiUrunTutari.ToString("n2") + " TL"
		'lblUrunKalemAdedi.Text = SepettekiUrunKalemAdedi

		'If Not IsPostBack Then
		'	SaydanClass._HDrpDoldur(drpSrch, "KATEGORILER", "KategoriKodu", "EbeveynKategoriKodu", "StokGrupAdi1", False, True)
		'End If

		'SideBarOlustur()
		'MenuyuOlustur()

	End Sub

	Protected Sub SideBarOlustur()
		Dim dtRaw As DataTable = SaydanClass._ConnDB("SELECT        KATEGORILER.KategoriKodu, KATEGORILER.StokGrupAdi1, KATEGORILER.EbeveynKategoriKodu, COUNT(KATEGORILER_URUNLER.KUrunKodu) AS UrunAdedi, KATEGORILER.Siralama FROM            KATEGORILER LEFT OUTER JOIN KATEGORILER_URUNLER ON KATEGORILER.KategoriKodu = KATEGORILER_URUNLER.KategoriKodu GROUP BY KATEGORILER.KategoriKodu, KATEGORILER.EbeveynKategoriKodu, KATEGORILER.StokGrupAdi1, KATEGORILER.Siralama ORDER BY KATEGORILER.Siralama, KATEGORILER.KategoriKodu")
		'Dim dv As DataView = dt.DefaultView

		Dim dt As DataTable = SaydanClass.Hiyerarsik_Tablo_Olusturma(dtRaw, "KategoriKodu", "EbeveynKategoriKodu")
		Dim cmdHTML As String = "<ul  id=""sideManu"" class=""nav nav-tabs nav-stacked"">"
		For siraNo = 0 To dt.Rows.Count - 1
			Dim drSiradakiKayit As DataRow = dt.Rows(siraNo)

			Dim Kod As Integer = drSiradakiKayit("KategoriKodu")
			Dim Level As Integer = drSiradakiKayit("__Level")

			If siraNo = dt.Rows.Count - 1 Then
				'eğer son kayda ulaştıysan (ardindaki kayit olmayacak
				'seviye 0 ise <li>xx</li>
				'seviye 1 ise <li>xx</li></ul></li>
				'Seviye 2 ise <li>xx</li> </ul></li> </ul></li>
				'Seviye 3 ise <li>xx</li> </ul></li> </ul></li> </ul></li>

				cmdHTML += vbCrLf + "<li>" + dt.Rows(siraNo)("StokGrupAdi1").ToString + "</li>"
				For Seviye = Level - 1 To 1 Step -1
					cmdHTML += vbCrLf + "</ul></li> "
				Next
			Else
				If dt.Rows(siraNo)("__Level") < dt.Rows(siraNo + 1)("__Level") Then
					'eğer ardındaki kaydin seviyesi daha yüksekse <li>xx<ul> ekle
					cmdHTML += vbCrLf + "<li class=""subMenu""><a href=""#"">" + dt.Rows(siraNo)("StokGrupAdi1").ToString + "</a><ul>"
				ElseIf dt.Rows(siraNo)("__Level") = dt.Rows(siraNo + 1)("__Level") Then
					'eğer ardındaki kaydin seviyesi aynı ise "<li>xx</li> şeklide bitir
					cmdHTML += vbCrLf + "<li><a href=""products.aspx?kod=" + dt.Rows(siraNo)("KategoriKodu").ToString + """>" + dt.Rows(siraNo)("StokGrupAdi1").ToString + "</a></li>"
				ElseIf dt.Rows(siraNo)("__Level") > dt.Rows(siraNo + 1)("__Level") Then
					'eğer ardındaki kaydin seviyesi düşükse<li>xx</li></ul></li> ile bitir
					cmdHTML += vbCrLf + "<li><a href=""products.aspx?kod=" + dt.Rows(siraNo)("KategoriKodu").ToString + """>" + dt.Rows(siraNo)("StokGrupAdi1").ToString + "</a></li>"
					Dim BirSonrakiSeviye As Integer = dt.Rows(siraNo + 1)("__Level")
					For Seviye = Level - 1 To BirSonrakiSeviye Step -1
						cmdHTML += vbCrLf + "</ul></li>"
					Next
				End If
			End If

		Next
		cmdHTML += vbCrLf + "</ul>"
		spnSideBar.InnerHtml = cmdHTML

	End Sub

	Protected Sub btnUyeGiris_Click(sender As Object, e As EventArgs) Handles btnUyeGiris.Click
		Dim cmdstr As String = "select * from [dbo].[MUSTERILER] where Email='" + inputEmail.Text + "' and Password='" + inputPassword.Text + "'"
		Dim dt As DataTable = SaydanClass._ConnDB(cmdstr)
		If dt.Rows.Count > 0 Then
			Dim dr As DataRow = dt.Rows(0)
			Session("UserID") = dr("MusteriKodu").ToString
			Session("UserName") = dr("Isim").ToString + " " + dr("Soyisim").ToString
			If IsDBNull(dr("YoneticiMi")) Then dr("YoneticiMi") = False
			Session("YoneticiMi") = dr("YoneticiMi")
			'Üye olmadan önce sepete yüklenen birşeyler varsa onların müşteri kodlarını güncelle ki müşterimizi uğraştırmayalım.
			cmdstr = "select * from SEPET where SessionID='" + Session("SessionID").ToString.ToString + "'"
			dt = SaydanClass._ConnDB(cmdstr)
			For Each dr In dt.Rows
				dr("MusteriKodu") = Session("UserID").ToString
			Next
			SaydanClass._SaveDB(cmdstr, dt)
		End If
		Response.Redirect(Me.Page.AppRelativeVirtualPath + "?" + Page.ClientQueryString)
	End Sub

	Protected Sub searchButton_Click(sender As Object, e As EventArgs) Handles searchButton.Click
		Session("SearchCriteria") = srchFldx.Text
		Response.Redirect("UrunArama.aspx")
	End Sub

	Protected Sub btnregister_Click(sender As Object, e As EventArgs) Handles btnregister.Click
		Session("LoginOncesiSayfa") = Page.AppRelativeVirtualPath + "?" + Page.ClientQueryString
		Response.Redirect("register.aspx")

	End Sub
End Class

