<%@ Page Title="" Language="VB" MasterPageFile="~/Master1.master" AutoEventWireup="false" CodeFile="UyeBilgileri.aspx.vb" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
		<div class="span9">
    <ul class="breadcrumb">
		<li><a href="index.aspx">Ana Sayfa</a> <span class="divider">/</span></li>
		<li><a href="UyeIslemleri.aspx">Üyelik İşlemleri</a> <span class="divider">/</span></li>
		<li class="active">Üye Bilgileri</li>
    </ul>
	<h3> Üyelik İşlemleri</h3>	
	<div class="well">
	<div class="container-fluid " >
		<h4>Kişisel Bilgileriniz</h4>
		<div class="control-group">
			<label class="control-label" for="txtIsim">İsim<sup>*</sup></label>
			<div class="controls">
			  <asp:TextBox id="txtIsim" placeholder="İsim" runat="server"  ></asp:TextBox> 
			</div>
		 </div>
		 <div class="control-group">
			<label class="control-label" for="txtSoyisim">Soyisim<sup>*</sup></label>
			<div class="controls">
				<asp:TextBox ID="txtSoyisim" runat="server"  placeholder="Soy İsim"></asp:TextBox>
			</div>
		 </div>
		 <div class="control-group">
			<label class="control-label" for="txtTCKimlikNo">TC Kimlik No<sup>*</sup></label>
			<div class="controls">
				<asp:TextBox ID="txtTCKimlikNo" runat="server"  placeholder="TC Kimlik No"></asp:TextBox>
			</div>
		 </div>
		<div class="control-group">
			<label class="control-label" for="txtphone">Telefon<sup>*</sup></label>
			<div class="controls">
			  <asp:TextBox id="txtphone" runat="server" placeholder="Telefon"/> 
			</div>
		</div>
		<div class="control-group">
		<label class="control-label" for="txtemail">Email <sup>*</sup></label>
		<div class="controls">
			<asp:TextBox ID="txtemail" runat="server" placeholder="Email"></asp:TextBox>
		</div>
	  </div>	  
	<div class="control-group">
		<label class="control-label" for="txtPassword">Şifre <sup>*</sup></label>
		<div class="controls">
			<asp:TextBox ID="txtPassword" runat="server" placeholder="Şifre"></asp:TextBox> Tekrar 
			<asp:TextBox ID="txtPassword2" runat="server" placeholder="Şifre"></asp:TextBox>
		</div>
	  </div>	  
		<hr />
		<h4>Teslimat Bilgileri</h4>
		<div class="control-group">
			<label class="control-label" for="txtTeslimatAdresi">Teslimat Adresi<sup>*</sup></label>
			<div class="controls">
				<asp:TextBox ID="txtTeslimatAdresi" runat="server" placeholder=" Teslimat Adresi" Width="100%" TextMode="MultiLine" ></asp:TextBox>
			</div>
		</div>
		<div class="control-group  " >
			<label class="control-label" for="txtTeslimatSehri">Teslimat Şehri<sup>*</sup></label>
			<div class="controls">
				<asp:TextBox ID="txtTeslimatSehri" runat="server" placeholder=" Teslimat Şehri"></asp:TextBox>
			</div>
		</div>
		
		<hr />
		<h4>Fatura Bilgileri</h4>

			<asp:CheckBox ID="chkFaturaAdresiAyni" runat="server" AutoPostBack="true"   /> Fatura Adresim Teslimat Adresimle Aynı<br />
		<div class="control-group  ">
			<label class="control-label" for="txtSirket">Firma Ünvanı</label>
			<div class="controls">
				<asp:TextBox ID="txtSirket" runat="server" placeholder="Şirket" Width="100%"></asp:TextBox>
			</div>
		</div>
		
		<div class="control-group  " >
			<label class="control-label" for="txtFaturaAdresi">Fatura Adresi<sup>*</sup></label>
			<div class="controls">

			  <asp:TextBox runat="server" id="txtFaturaAdresi" placeholder=" Fatura Adresi" width="100%" TextMode="MultiLine"  /> 
			</div>
		</div>
		
		<div class="control-group ">
			<label class="control-label" for="txtVergiDairesi">Vergi Dairesi</label>
			<div class="controls">
			  <asp:TextBox id="txtVergiDairesi" runat="server" placeholder=" Vergi Dairesi"/> 
			</div>
		</div>
				<div class="control-group  ">
			<label class="control-label" for="txtVergiNo">Vergi Numarası</label>
			<div class="controls">
			  <asp:TextBox id="txtVergiNo" runat="server" placeholder=" Vergi Numarası"/> 
			</div>
		</div>
		<hr />
		<div class="control-group">
			<label class="control-label" for="txtEkBilgiler">Ek Bilgiler</label>
			<div class="controls">
			  <asp:TextBox  runat="server" TextMode="MultiLine" Width="100%" id="txtEkBilgiler" placeholder="Ek Bilgiler" ></asp:TextBox>
			</div>
		</div>
<%--		<div class="control-group">
			<div class="controls">
				<asp:CheckBox ID="chkSozlesmeOnay" runat="server" AutoPostBack="true"  /><b>
				<asp:HyperLink id="lblSozlesmeOnay" runat="server" NavigateUrl="~/tac.aspx" Target="_blank" ForeColor="Red"   >Üyelik Sözleşmesini </asp:HyperLink>Onaylıyorum</b>
			</div>
		</div>--%>
		



	<div class="alert alert-block alert-error fade in" id="divUyari" runat="server" visible="false"  >
		<asp:Linkbutton CssClass ="close" data-dismiss="alert" ID="btnUyari" runat="server" >x</asp:Linkbutton>
		<asp:Label ID="lblUyari" runat="server" >
			<strong>Lorem Ipsum is simply</strong> dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s
		</asp:Label>
	 </div>	

		
	
	<div class="control-group">
			<div class="controls">
				<a href="uyeislemleri.aspx" class="btn btn-large"><i class="icon-arrow-left"></i> Geri Dön</a>
				<asp:LinkButton ID="btnRegister" runat="server" CssClass="btn btn-large btn-default">Kaydet</asp:LinkButton>
			</div>
		</div>		
	<p><sup>*</sup>Girilmesi Zorunlu Alanlar</p>
	</div>
</div>

</div>

</asp:Content>

