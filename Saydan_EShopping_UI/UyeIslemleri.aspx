<%@ Page Title="" Language="VB" MasterPageFile="~/Master1.master" AutoEventWireup="false" CodeFile="UyeIslemleri.aspx.vb" Inherits="UyeIslemleri" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


	<div class="span9">
						<ul class="breadcrumb">
							<li><a href="index.aspx">Ana Sayfa</a> <span class="divider">/</span></li>
							<li>Üye İşlemleri</li>
						</ul>

						<hr class="soft" />

						<div>
							<p><a href="UyeBilgileri.aspx">Üyelik Bilgileri</a><br />							</p>
							<p><a href="SiparisListe.aspx">Geçmiş Siparişlerim</a><br />							</p>
							<p><a href="SiparisListe_OdemeBekleyenler.aspx">Ödeme Bekleyen Siparişler</a><br />							</p>
							<p><a href="SiparisIade.aspx">İptal ve İade İşlemleri</a><br />							</p>
							<p><asp:LinkButton ID="lnkUyeCikis" runat="server"  >Güvenli Çıkış</asp:LinkButton><br />							</p>
						</div>
						<br />
						<br />

						<asp:Panel ID="pnlYonetici" runat="server" ForeColor="red">

							<p><a href="SiparisListeYonetici.aspx"><span style="color: red;">İşlemdeki Siparişler</span></a></p>
							<p><a href="KategorilerListe.aspx"><span style="color: red;">Kategori Tanımları</span></a></p>
							<p><a href="UrunlerListe.aspx"><span style="color: red;">Ürün Tanımları</span></a></p>
							<p><a href="BannerListe.aspx"><span style="color: red;">Ana Sayfa Slaytları</span></a></p>
							<p><a href="XMLUpdate.aspx"><span style="color: red;">XML Update</span></a></p>
						</asp:Panel>

	</div>

</asp:Content>

