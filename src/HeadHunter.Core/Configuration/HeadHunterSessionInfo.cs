namespace HeadHunter.Core.Configuration;

public class HeadHunterSessionInfo
{
    public HeadHunterSessionInfo()
    { }

    public HeadHunterSessionInfo(string xsrfToken, string hhUid, string hhToken)
    {
        XsrfToken = xsrfToken;
        HhUid = hhUid;
        HhToken = hhToken;
    }

    public string XsrfToken { get; set; }
    public string HhUid { get; set; }
    public string HhToken { get; set; }

    public string Cookie => $"xsrf={XsrfToken};hhuid={HhUid};hhtoken={HhToken}";
}