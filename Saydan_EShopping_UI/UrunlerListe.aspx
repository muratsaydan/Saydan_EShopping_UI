<%@ Page Title="" Language="VB" MasterPageFile="~/Master2.master" AutoEventWireup="false" CodeFile="UrunlerListe.aspx.vb" Inherits="UrunlerListe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

	<div id="mainBody">
	<div class="container">
	<div class="row">
		<div class="span12">		


   <ul class="breadcrumb">
		<li><a href="index.aspx">Ana Sayfa</a> <span class="divider">/</span></li>
		<li><a href="UyeIslemleri.aspx">Üye İşlemleri</a> <span class="divider">/</span></li>
		<li class="active"> Ürün Listesi</li>
    </ul>
	<hr class="soft"/>
			<h3>Ürün Listesi : </h3>
			<p>
				Stok Adı : <asp:TextBox ID="txtStokAdi" runat="server" ></asp:TextBox>
				Stok Kodu : <asp:TextBox ID="txtStokKodu" runat="server" ></asp:TextBox>
				Tedarikçi Firma : <asp:TextBox ID="txtTedarikci" runat="server" ></asp:TextBox>
				Barkod No : <asp:TextBox ID="txtBarkodNo" runat="server" ></asp:TextBox>
				Kayıt Adedi: <asp:TextBox ID="txtKayitAdedi" runat="server" TextMode="Number"  >100</asp:TextBox>
			</p>
			<asp:HyperLink ID="btnyeniekle" runat="server" NavigateUrl="~/UrunlerDetay.aspx?kod=-1" target="_blank"  >Yeni Ürün</asp:HyperLink><br />
			<p>Sistemde toplam&nbsp;&nbsp;<asp:Label ID="lblUrunAdedi" runat="server" >123</asp:Label>&nbsp;&nbsp; adet ürün bulunmaktadır.</p>
			<table class="table table-bordered" id="tblKategoriler" runat="server"></table>
			<asp:HyperLink ID="hypPrev" runat="server" ><</asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:TextBox ID="txtSayfaNo" runat="server" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:HyperLink ID="hypNext" runat="server" >></asp:HyperLink>
		</div>
		</div>
	</div>
</div>







</asp:Content>

