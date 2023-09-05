<%@ Page Title="" Language="VB" MasterPageFile="~/Master2.master" AutoEventWireup="false" CodeFile="KategorilerListe.aspx.vb" Inherits="KategorilerListe" %>

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
		<li class="active"> Kategoriler Listesi</li>
    </ul>
	<hr class="soft"/>
			<h3>Kategori Listesi : </h3>
			<table class="table table-bordered" id="tblKategoriler" runat="server"></table>

		</div>
		</div>
	</div>
</div>







</asp:Content>

