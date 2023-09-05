Imports System.Data

Partial Class Masterz
	Inherits System.Web.UI.MasterPage

	Dim SaydanClass As New SaydanClass


	Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
		If Session("UserID") = Nothing Then
			Session("UserID") = 1
			Session("UserName") = "Murat Saydan"
		End If
	End Sub

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		Dim dtSepet As System.Data.DataTable = SaydanClass._ConnDB("SELECT count(*) UrunAdedi, sum(URUNLER.SatisFiyati* SEPET.Miktar) as Tutar FROM SEPET INNER JOIN URUNLER ON SEPET.UrunKodu = URUNLER.StokKodu WHERE SEPET.MusteriKodu = " + Session("UserID").ToString)
		Dim SepettekiUrunKalemAdedi As Integer = dtSepet.Rows(0)(0)
		Dim SepettekiUrunTutari As Integer = dtSepet.Rows(0)(1)
		lblSepettekiUrunTutari.Text = SepettekiUrunTutari.ToString("n2") + " TL"
		lblUrunKalemAdedi.Text = SepettekiUrunKalemAdedi
		lblSepettekiUrunTutari2.Text = SepettekiUrunTutari.ToString("n2") + " TL"
		lblUrunKalemAdedi2.Text = SepettekiUrunKalemAdedi
		lblUserID.Text = Session("UserName")

		SideBarOlustur()
		'MenuyuOlustur()

	End Sub

	Protected Sub MenuyuOlustur()
		Dim dt As DataTable = SaydanClass._ConnDB("SELECT KATEGORILER.KategoriKodu, KATEGORILER.StokGrupAdi1, KATEGORILER.EbeveynKategoriKodu, COUNT(KATEGORILER_URUNLER.KUrunKodu) AS UrunAdedi FROM KATEGORILER LEFT OUTER JOIN KATEGORILER_URUNLER ON KATEGORILER.KategoriKodu = KATEGORILER_URUNLER.KategoriKodu GROUP BY KATEGORILER.KategoriKodu, KATEGORILER.EbeveynKategoriKodu, KATEGORILER.StokGrupAdi1")
		Dim dv As DataView = dt.DefaultView

		Dim dt2 As DataTable = SaydanClass.Hiyerarsik_Tablo_Olusturma(dt, "KategoriKodu", "EbeveynKategoriKodu")
		Dim span As String = "<nav>"
		Dim seviye As Integer = -1
		Dim anaMenu As Boolean = True
		Dim ilkliMi As Boolean = True
		For i = 0 To dt2.Rows.Count - 1
			Dim dr As DataRow = dt2.Rows(i)

			Dim level As Integer = dr("__Level")
			If seviye < level Then
				If anaMenu Then
					span += vbCrLf + "<ul id=""nav"" class=""nav"">"
					anaMenu = False
				Else
					span += vbCrLf + "<ul>"
				End If
			ElseIf seviye > level Then
				'If seviye = 0 Then
				'    span += vbCrLf + "</ul>"
				'Else
				span += "</li>"
				Do Until seviye = level
					span += vbCrLf + "</ul>" + vbCrLf + "</li>"
					seviye -= 1
				Loop
				'End If
			ElseIf seviye = level Then
				span += "</li>"
			End If
			seviye = level
			Dim resim As String = ""
			'If Not IsDBNull(dr("Resim")) Then resim = "<i class=""fa fa-lg fa-fw " + dr("Resim").ToString + """></i>"
			Dim AnaSeviye1 As String = ""
			Dim AnaSeviye2 As String = ""
			If seviye = 1 Then
				AnaSeviye1 = "<span class=""menu-item-parent"">"
				AnaSeviye2 = "</span>"
			End If
			If i + 1 <= dt2.Rows.Count - 1 Then
				If level < dt2.Rows(i + 1)("__Level") Then  'Alt Kırılımı Varsa
					span += vbCrLf + "<li><a name="""">" + resim + AnaSeviye1 + dr("MenuAdi_TRK").ToString + AnaSeviye2 + "</a>"
				Else
					'If dr("URLAdresi").ToString = "" Then
					'	'dr("URLAdresi") = "#"
					'Else
					span += vbCrLf + "<li><a href=""" + dr("KategoriKodu").ToString + """>" + resim + AnaSeviye1 + dr("MenuAdi_TRK").ToString + AnaSeviye2 + "</a>"
					'End If
				End If
			Else
				'If dr("URLAdresi").ToString = "" Then
				'	dr("URLAdresi") = "#"
				'Else
				span += vbCrLf + "<li><a href=""" + dr("KategoriKodu").ToString + """ >" + resim + AnaSeviye1 + dr("MenuAdi_TRK").ToString + AnaSeviye2 + "</a>"
				'End If
			End If
		Next
		Do Until seviye <= 1
			span += vbCrLf + "</ul>" + vbCrLf + "</li>"
			seviye -= 1
		Loop



		spnSideBar.InnerHtml = span + vbCrLf + "</ul></nav>"
	End Sub

	Protected Sub SideBarOlustur()
		Dim dtRaw As DataTable = SaydanClass._ConnDB("SELECT KATEGORILER.KategoriKodu, KATEGORILER.StokGrupAdi1, KATEGORILER.EbeveynKategoriKodu, COUNT(KATEGORILER_URUNLER.KUrunKodu) AS UrunAdedi FROM KATEGORILER LEFT OUTER JOIN KATEGORILER_URUNLER ON KATEGORILER.KategoriKodu = KATEGORILER_URUNLER.KategoriKodu GROUP BY KATEGORILER.KategoriKodu, KATEGORILER.EbeveynKategoriKodu, KATEGORILER.StokGrupAdi1")
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
End Class

