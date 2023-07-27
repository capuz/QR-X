param([string]$buildnumber='0') #Must be the first statement in your script

$filename = 'C:\inetpub\wwwroot\PortalDOM\web.config'
$filename2 = 'C:\inetpub\wwwroot\OficinaDOM\web.config'
$filename3 = 'C:\inetpub\wwwroot\WebApiDOM\web.config'



$content = [System.IO.File]::ReadAllText($filename).Replace("http://webApiDom/api/","http://minv0013.cloudapp.net/WebApiDOM/api/")
[System.IO.File]::WriteAllText($filename, $content)

$content = [System.IO.File]::ReadAllText($filename2).Replace("http://webapidom/api/","http://minv0013.cloudapp.net/WebApiDOM/api/")
[System.IO.File]::WriteAllText($filename2, $content)

$content = [System.IO.File]::ReadAllText($filename3).Replace("10.0.2.55\sql2008","MINV0013")
[System.IO.File]::WriteAllText($filename3, $content)
