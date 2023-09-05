<%@ Page Title="" Language="VB" MasterPageFile="~/Master1.master" AutoEventWireup="false" CodeFile="SiparisIadeDetay.aspx.vb" Inherits="SiparisIadeDetay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<div class="span9">
    <ul class="breadcrumb">
		<li><a href="index.aspx">Ana Sayfa</a> <span class="divider">/</span></li>
		<li class="active"> Teslimat Bilgileri</li>
    </ul>
	<hr class="soft"/>

			<table class="table table-bordered" id="tbl1" runat="server">
			</table>

				<div class="well">
	<div class="container-fluid " >
		<h4>İade Sebebi ve Onay</h4>
		<div class="control-group">
			<label class="control-label" for="txtTeslimatAdresi" style="font-weight:bold;">İade sebebinizi öğrenebilir miyiz?</label>
			<div class="controls">
				<asp:DropDownList ID="drpIadeSebebi" runat="server" Width="100%" >
					<asp:ListItem>Seçiniz</asp:ListItem>
					<asp:ListItem>Ürünleri daha ucuza başka bir sitede buldum</asp:ListItem>
					<asp:ListItem>Ürünleri beğenmedim</asp:ListItem>
					<asp:ListItem>Hizmetinizden memnun olmadım</asp:ListItem>
					<asp:ListItem>Siteniz güven telkin etmedi</asp:ListItem>
					<asp:ListItem>Diğer</asp:ListItem>
				</asp:DropDownList>
			</div>
		</div>
		<div class="control-group  " >
			<label class="control-label" for="txtTeslimatSehri" style="font-weight:bold;>İade Açıklaması</label>
			<div class="controls">
				<asp:TextBox ID="txtAciklama" runat="server" TextMode="MultiLine"   Width="100%" ></asp:TextBox>
			</div>
		</div>
		
	</div>
</div>


			
	<a href="SiparisIade.aspx" class="btn btn-large"><i class="icon-arrow-left"></i> Geri Dön</a>
	<asp:LinkButton ID="btnIade" runat="server" CssClass="btn btn-large pull-right">İade Et </asp:LinkButton>
	
</div>

</asp:Content>

