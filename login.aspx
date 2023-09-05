<%@ Page Title="" Language="VB" EnableEventValidation="false"  MasterPageFile="~/Master1.master" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<div class="span9">
    <ul class="breadcrumb">
		<li><a href="index.aspx">Ana Sayfa</a> <span class="divider">/</span></li>
		<li class="active">Kullanıcı Girişi</li>
    </ul>
	<h3> Kullanıcı Girişi</h3>	
	<hr class="soft"/>
	
	<div class="row">
		<div class="span4" style="height:250px">
			<div class="well">
<%--			<h5>YENİ BİR KULLANICI TANIMLA</h5><br/>--%>
			<br/><br/><br/><br/><br/>
			<div >
			  <div class="controls" style="text-align:center; ">
			  <asp:LinkButton ID="btnregister" runat="server" class="btn block" >Yeni Kullanıcı Tanımla</asp:LinkButton> 
				  <br/><br/><br/><br/><br/><br/>
			  </div>
			</div>
		</div> &nbsp;
		</div>
		<div class="span1"> &nbsp;</div>
		<div class="span4" style="height:250px">
			<div class="well">
			<h5>MEVCUT KULLANICI GİRİŞİ</h5>
			<asp:Panel ID="pnl1" runat="server" DefaultButton="btnUyeGiris">
			  <div class="control-group">
				<label class="control-label" for="inputEmail1">Email</label>
				<div class="controls">
				  <asp:TextBox CssClass ="span3"  id="inputEmail1" placeholder="Email" runat="server" ></asp:TextBox> 
				</div>
			  </div>
			  <div class="control-group">
				<label class="control-label" for="inputPassword1">Şifre</label>
				<div class="controls">
				  <asp:TextBox TextMode="password" CssClass ="span3" runat="server" id="inputPassword1" placeholder="Şifre"> </asp:TextBox> 
				</div>
			  </div>
			  <div class="control-group">
				<div class="controls">
				  <asp:LinkButton ID="btnUyeGiris" runat="server" CssClass="btn">Giriş</asp:LinkButton> <a href="forgetpass.aspx">Şifremi unuttum</a>
				</div>
			  </div>
			</asp:Panel>
		</div>
		</div>
	</div>	
	
</div>

</asp:Content>

