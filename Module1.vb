Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json

Module Module1

    Sub Main()
        '  MsgBox("Hello, World!")

        Console.WriteLine("Hello World")


        ' removed client_secret, add back in to make functional
        ' removed client_secret, add back in to make functional
        Dim postData As String = "client_id=FAKEID&client_secret=FAKESECRET"
        Dim tempCookies As New CookieContainer
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("https://api.hs.homedepot.com/iconx/v1/auth/accesstoken?grant_type=client_credentials"), HttpWebRequest)
        postReq.Method = "POST"
        ' postReq.KeepAlive = True
        ' postReq.CookieContainer = tempCookies
        postReq.ContentType = "application/x-www-form-urlencoded"
        postReq.Referer = "http://www.danmer.com/"
        postReq.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
        postReq.ContentLength = byteData.Length

        Dim postreqstream As Stream = postReq.GetRequestStream()
        postreqstream.Write(byteData, 0, byteData.Length)
        postreqstream.Close()
        Dim postresponse As HttpWebResponse

        postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)
        ' tempCookies.Add(postresponse.Cookies)
        ' logincookie = tempCookies
        Dim postreqreader As New StreamReader(postresponse.GetResponseStream())

        Dim thepage As String = postreqreader.ReadToEnd

        Dim decodedresponse = JsonConvert.DeserializeObject(Of client_credentials)(thepage)

        Console.WriteLine(decodedresponse.access_token)
        
    End Sub

End Module
