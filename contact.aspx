<%@ Page Title="" Language="VB" MasterPageFile="~/Master2.master" AutoEventWireup="false" CodeFile="contact.aspx.vb" Inherits="contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<div class="container">

	<hr class="soften">
	<h1>Bizi Ziyaret Edin</h1>
	<hr class="soften"/>	
	<div class="row">
		<div class="span4">
		<h4>İletişim Bilgileri</h4>
		<p>	Viranşehir Mah. 34388 Sok. Adonis Sit. 
<br/> Nergis Apt. No: 1 D2 / 15 MEZİTLİ / MERSİN 33340
			<br/><br/>
			Mail :info@mersinborsasi.com<br/>
			﻿Tel :0 850 840 85 21<br/>
			Fax :0 850 840 85 21<br/>
			Web : www.mersinborsasi.com
		</p>		
		</div>
			
		<div class="span4">
		<h4>Çalışma Saatleri</h4>
			<h5> Pazatesi - Cuma</h5>
			<p>09:30 - 19:00<br/><br/></p>
			<h5>Saturday</h5>
			<p>09:30 - 12:00<br/><br/></p>
			<h5>Pazar</h5>
			<p>12:30 - 15:00<br/><br/></p>
		</div>
		<div class="span4">
		<h4>Email Gönderin</h4>
		<form class="form-horizontal">
        <fieldset>
          <div class="control-group">
           
              <input type="text" placeholder="İsim" class="input-xlarge"/>
           
          </div>
		   <div class="control-group">
           
              <input type="text" placeholder="email" class="input-xlarge"/>
           
          </div>
		   <div class="control-group">
           
              <input type="text" placeholder="konu" class="input-xlarge"/>
          
          </div>
          <div class="control-group">
              <textarea rows="3" id="textarea" class="input-xlarge"></textarea>
           
          </div>

            <button class="btn btn-large" type="submit">Gönder</button>

        </fieldset>
      </form>
		</div>
	</div>
	<div class="row">
	<div class="span12">
	<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3197.3679894197035!2d34.514440315624675!3d36.73773727891222!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x15278a0c2c5d9f07%3A0x877e7082bd4d7cb1!2sMerkez%2C+Gazi+Mustafa+Kemal+Blv.+No%3A882%2C+33330+Mezitli%2FMersin%2C+T%C3%BCrkiye!5e0!3m2!1str!2suk!4v1466161047514" width="1170" height="200" frameborder="0" style="border:0" allowfullscreen></iframe>
		<br />
	</div>
	</div>
</div>
</asp:Content>

