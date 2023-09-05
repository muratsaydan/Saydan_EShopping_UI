Imports System.Data


Partial Class Master1
	Inherits System.Web.UI.MasterPage

	Dim SaydanClass As New SaydanClass

	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load


		Dim whrcnd As String
		If Not Session("UserID") = Nothing Then
			whrcnd = "SEPET.MusteriKodu = " + Session("UserID").ToString
		Else
			whrcnd = "SEPET.SessionID = '" + Session("SessionID").ToString.ToString + "'"
		End If
		Dim dtSepet As System.Data.DataTable = SaydanClass._ConnDB("select count(*) adet from (SELECT distinct [UrunKodu] FROM [SEPET] where " + whrcnd + " group by UrunKodu ) as A")
		Dim SepettekiUrunKalemAdedi As Integer = dtSepet.Rows(0)(0)
		dtSepet = SaydanClass._ConnDB("SELECT SUM(URUNLER.SatisFiyati * SEPET.Miktar) AS Tutar FROM SEPET INNER JOIN URUNLER ON SEPET.UrunKodu = URUNLER.StokKodu where " + whrcnd)
		Dim SepettekiUrunTutari As Integer = 0
		If dtSepet.Rows.Count > 0 Then
			If IsDBNull(dtSepet.Rows(0)(0)) Then dtSepet.Rows(0)(0) = 0
			SepettekiUrunTutari = dtSepet.Rows(0)(0)
		End If

		lblSepettekiUrunTutari2.Text = SepettekiUrunTutari.ToString("n2") + " TL"

		lblUrunKalemAdedi2.Text = SepettekiUrunKalemAdedi



		SideBarOlustur()

	End Sub
	'Protected Sub SideBarOlustur()
	'	Dim dtRaw As DataTable = SaydanClass._ConnDB("SELECT KATEGORILER.KategoriKodu, KATEGORILER.StokGrupAdi1, KATEGORILER.EbeveynKategoriKodu, COUNT(KATEGORILER_URUNLER.KUrunKodu) AS UrunAdedi FROM KATEGORILER LEFT OUTER JOIN KATEGORILER_URUNLER ON KATEGORILER.KategoriKodu = KATEGORILER_URUNLER.KategoriKodu GROUP BY KATEGORILER.KategoriKodu, KATEGORILER.EbeveynKategoriKodu, KATEGORILER.StokGrupAdi1")
	'	'Dim dv As DataView = dt.DefaultView

	'	Dim dt As DataTable = SaydanClass.Hiyerarsik_Tablo_Olusturma(dtRaw, "KategoriKodu", "EbeveynKategoriKodu")
	'	Dim cmdHTML As String = "<ul  id=""sideManu"" class=""nav nav-tabs nav-stacked"">"
	'	For siraNo = 0 To dt.Rows.Count - 1
	'		Dim drSiradakiKayit As DataRow = dt.Rows(siraNo)

	'		Dim Kod As Integer = drSiradakiKayit("KategoriKodu")
	'		Dim Level As Integer = drSiradakiKayit("__Level")

	'		If siraNo = dt.Rows.Count - 1 Then
	'			'eğer son kayda ulaştıysan (ardindaki kayit olmayacak
	'			'seviye 0 ise <li>xx</li>
	'			'seviye 1 ise <li>xx</li></ul></li>
	'			'Seviye 2 ise <li>xx</li> </ul></li> </ul></li>
	'			'Seviye 3 ise <li>xx</li> </ul></li> </ul></li> </ul></li>

	'			cmdHTML += vbCrLf + "<li>" + dt.Rows(siraNo)("StokGrupAdi1").ToString + "</li>"
	'			For Seviye = Level - 1 To 1 Step -1
	'				cmdHTML += vbCrLf + "</ul></li> "
	'			Next
	'		Else
	'			If dt.Rows(siraNo)("__Level") < dt.Rows(siraNo + 1)("__Level") Then
	'				'eğer ardındaki kaydin seviyesi daha yüksekse <li>xx<ul> ekle
	'				cmdHTML += vbCrLf + "<li class=""subMenu""><a href=""#"">" + dt.Rows(siraNo)("StokGrupAdi1").ToString + "</a><ul>"
	'			ElseIf dt.Rows(siraNo)("__Level") = dt.Rows(siraNo + 1)("__Level") Then
	'				'eğer ardındaki kaydin seviyesi aynı ise "<li>xx</li> şeklide bitir
	'				cmdHTML += vbCrLf + "<li><a href=""products.aspx?kod=" + dt.Rows(siraNo)("KategoriKodu").ToString + """>" + dt.Rows(siraNo)("StokGrupAdi1").ToString + "</a></li>"
	'			ElseIf dt.Rows(siraNo)("__Level") > dt.Rows(siraNo + 1)("__Level") Then
	'				'eğer ardındaki kaydin seviyesi düşükse<li>xx</li></ul></li> ile bitir
	'				cmdHTML += vbCrLf + "<li><a href=""products.aspx?kod=" + dt.Rows(siraNo)("KategoriKodu").ToString + """>" + dt.Rows(siraNo)("StokGrupAdi1").ToString + "</a></li>"
	'				Dim BirSonrakiSeviye As Integer = dt.Rows(siraNo + 1)("__Level")
	'				For Seviye = Level - 1 To BirSonrakiSeviye Step -1
	'					cmdHTML += vbCrLf + "</ul></li>"
	'				Next
	'			End If
	'		End If

	'	Next
	'	cmdHTML += vbCrLf + "</ul>"
	'	spnSideBar.InnerHtml = cmdHTML

	'End Sub

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

				cmdHTML += vbCrLf + "<li> <a href=""products.aspx?kod=" + dt.Rows(siraNo)("KategoriKodu").ToString + """>" + dt.Rows(siraNo)("StokGrupAdi1").ToString + "</a></li>"
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

