<%@ Import Namespace="System.Data" %>

<%@ Application Language="VB" %>

<script runat="server">
    Dim Saydanclass As New SaydanClass

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Session("SessionID") = Now.ToString("dd.MM.yyyy HH:mm:ss.ffff ") + CInt(Rnd(100000) * 100000).ToString

        'Dim cmdstr As String = "select top 1 * from ISLEM_LOGLARI"
        'Dim dt As DataTable = Saydanclass._ConnDB(cmdstr)
        'Dim dr As DataRow = dt.NewRow
        'dr("Tarih") = Now
        'dr("SessionID") = Session("SessionID")
        'dr("IslemTipi") = "Kullanıcı Girişi"
        'dr("IpAdresi") = Request.UserHostAddress
        'If Not Session("UserID") Is Nothing Then dr("MusteriKodu") = Session("UserID")
        'dt.Rows.Add(dr)
        'Saydanclass._SaveDB(cmdstr, dt)


        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub

</script>