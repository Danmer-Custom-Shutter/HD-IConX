Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json

Module Module1

    Sub Main()

        Dim XmlBody As String

        Console.WriteLine("Hello World")

        Dim app_token As String = auth_accesstoken()

        Console.WriteLine(app_token)

        Console.WriteLine(poworklist_summary(app_token))

        XmlBody = "<?xml version='1.0' encoding='UTF-8'?><poWorklistListRequest ><search></search><sortCriterias><sortCriteria><sortBy>poNumber</sortBy><sortOrder>ASC</sortOrder></sortCriteria></sortCriterias></poWorklistListRequest >"

        Console.WriteLine(poworklist_list(app_token, XmlBody))

        XmlBody = "<?xml version='1.0' encoding='UTF-8'?><poGetByIdRequest><poNumber>03417796</poNumber><storeNumber>0603</storeNumber></poGetByIdRequest>"

        Console.WriteLine(pooverview_get_id(app_token, XmlBody))

        Console.WriteLine("That's All Folks")


    End Sub


    ''' <summary>
    ''' The purpose of this service to get a time limited access_token for signing future requests
    ''' </summary>
    ''' <returns>access_token</returns>
    ''' <remarks></remarks>
    ''' 
    Public Function auth_accesstoken()

        ' Setup POST Body
        Dim postData As String = "client_id=" + client_keys.client_id + "&client_secret=" + client_keys.client_secret
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        ' Setup Basic Request
        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("https://api.hs.homedepot.com/iconx/v1/auth/accesstoken?grant_type=client_credentials"), HttpWebRequest)
        postReq.Method = "POST"
        postReq.ContentType = "application/x-www-form-urlencoded"
        postReq.Referer = "http://www.danmer.com/"
        postReq.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
        postReq.ContentLength = byteData.Length

        ' Send Body of POST Request
        Dim postreqstream As Stream = postReq.GetRequestStream()
        postreqstream.Write(byteData, 0, byteData.Length)
        postreqstream.Close()
        Dim postresponse As HttpWebResponse

        ' Do request
        postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)

        ' Read response
        Dim postreqreader As New StreamReader(postresponse.GetResponseStream())

        Dim results As String = postreqreader.ReadToEnd

        ' extract out values from JSON to an Object
        Dim decodedresponse = JsonConvert.DeserializeObject(Of client_credentials)(results)

        ' Return access_token
        Return decodedresponse.access_token

    End Function

    ''' <summary>
    ''' The purpose of this service to provide high level synopsis of the PO Worklist items. It provides the following information, more details would be defined in the Response structure.
    ''' </summary>
    ''' <returns>XML Data</returns>
    ''' <remarks></remarks>
    Public Function poworklist_summary(ByVal app_token As String)

        Dim encoding As New UTF8Encoding
        Dim getresponse As HttpWebResponse

        Dim getReq As HttpWebRequest = DirectCast(WebRequest.Create("https://api.hs.homedepot.com/iconx/v1/poworklist/summary"), HttpWebRequest)
        getReq.ContentType = "application/xml"
        getReq.Referer = "http://www.danmer.com/"
        getReq.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
        getReq.Headers.Add("appToken: " + app_token)

        getresponse = DirectCast(getReq.GetResponse(), HttpWebResponse)

        Dim getreqreader As New StreamReader(getresponse.GetResponseStream())

        Dim results As String = getreqreader.ReadToEnd

        Return results
    End Function

    ''' <summary>
    ''' The service provides the Search capabilities against the PO Worklist items and provides the list of the Worklist items based on the criteria. Use this method to retrieve the individual POs from worklist either with or without a search criteria. This service provides the capability to Search for PO Worklist items by any of the following criteria.
    ''' </summary>
    ''' <param name="app_token">App_token</param>
    ''' <param name="XMLBody">XML Body</param>
    ''' <returns>XML Response</returns>
    ''' <remarks></remarks>
    Public Function poworklist_list(ByVal app_token As String, ByVal XmlBody As String)

        ' Setup POST Body
        Dim postData As String = XmlBody
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        ' Setup Basic Request
        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("https://api.hs.homedepot.com/iconx/v1/poworklist/list"), HttpWebRequest)
        postReq.Method = "POST"
        postReq.ContentType = "application/xml"
        postReq.Referer = "http://www.danmer.com/"
        postReq.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
        postReq.ContentLength = byteData.Length
        postReq.Headers.Add("appToken: " + app_token)

        ' Send Body of POST Request
        Dim postreqstream As Stream = postReq.GetRequestStream()
        postreqstream.Write(byteData, 0, byteData.Length)
        postreqstream.Close()
        Dim postresponse As HttpWebResponse

        ' Do request
        postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)

        ' Read response
        Dim postreqreader As New StreamReader(postresponse.GetResponseStream())

        Dim results As String = postreqreader.ReadToEnd

        Return results

    End Function


    ''' <summary>
    ''' PO Details service that provides the following capabilities:
    ''' </summary>
    ''' <param name="app_token">App_token</param>
    ''' <param name="XMLBody">XML Body</param>
    ''' <returns>XML Response</returns>
    ''' <remarks></remarks>
    Public Function pooverview_get_id(ByVal app_token As String, ByVal XmlBody As String)

        ' Setup POST Body
        Dim postData As String = XmlBody
        Dim encoding As New UTF8Encoding
        Dim byteData As Byte() = encoding.GetBytes(postData)

        ' Setup Basic Request
        Dim postReq As HttpWebRequest = DirectCast(WebRequest.Create("https://api.hs.homedepot.com/iconx/v1/pooverview/get/id"), HttpWebRequest)
        postReq.Method = "POST"
        postReq.ContentType = "application/xml"
        postReq.Referer = "http://www.danmer.com/"
        postReq.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
        postReq.ContentLength = byteData.Length
        postReq.Headers.Add("appToken: " + app_token)

        ' Send Body of POST Request
        Dim postreqstream As Stream = postReq.GetRequestStream()
        postreqstream.Write(byteData, 0, byteData.Length)
        postreqstream.Close()
        Dim postresponse As HttpWebResponse

        ' Do request
        postresponse = DirectCast(postReq.GetResponse(), HttpWebResponse)

        ' Read response
        Dim postreqreader As New StreamReader(postresponse.GetResponseStream())

        Dim results As String = postreqreader.ReadToEnd

        Return results

    End Function






End Module
