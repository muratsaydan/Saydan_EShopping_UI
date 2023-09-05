<%@ Page Title="" Language="VB" MasterPageFile="~/Master1.master" AutoEventWireup="false" CodeFile="OdemeSayfasi.aspx.vb" Inherits="OdemeSayfasi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<div class="span9">
		<ul class="breadcrumb">
			<li><a href="index.aspx">Ana Sayfa</a> <span class="divider">/</span></li>
			<li><asp:HyperLink id="lnkSiparisDetay" runat="server" >Sipariş Bilgileri</asp:HyperLink> <span class="divider">/</span></li>
			<li><asp:HyperLink id="lnkTeslimatDetay" runat="server" >Teslimat Bilgileri</asp:HyperLink> <span class="divider">/</span></li>
			<li class="active">Ödeme Sayfası</li>
		</ul>
		<hr class="soft" />

<%--		<div style="padding-top: 10em; padding-left: 40em;">
		</div>--%>
		<div id="iyzipay-checkout-form" class="responsive"></div>








	</div>

</asp:Content>

