<%@ Page Title="" Language="VB" MasterPageFile="~/Master1.master" AutoEventWireup="false" CodeFile="SiparisListe.aspx.vb" Inherits="SiparisListe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<div class="span9">
    <ul class="breadcrumb">
		<li><a href="index.aspx">Ana Sayfa</a> <span class="divider">/</span></li>
		<li class="active"> Verilen Siparişler</li>
    </ul>
	<hr class="soft"/>

			<table class="table table-bordered" id="tbl1" runat="server">
			</table>

</div>

</asp:Content>

