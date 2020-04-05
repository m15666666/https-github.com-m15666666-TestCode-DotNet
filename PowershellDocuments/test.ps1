<# change current dir to where powershell script file is  #>
function ChangeCurrentDir($path) {
    $dir = Split-Path -Parent $path
    Set-Location $dir
}

<# read json config file#>
function ReadJson($path) {
    return Get-Content $path | ConvertFrom-Json
}

<# 检查指定路径的进程是否运行 #>
function CheckRunning($processName, $path) {
    $path = $path.ToLower()
    try 
    {
        $processes = Get-Process -name $processName -ErrorAction SilentlyContinue
        foreach( $p in $processes){
            if((!$p.HasExited) -and $p.Path.ToLower().Equals($path) ){
                return $true
            }
        }
    }
    catch 
    {
        Write-Output "catch stop exception."
    }
    return $false
    <#
    Get-Process -name $processName | ForEach-Object {
        if ($_.Path.ToLower().Equals($path)) 
        { "{0}({1})" -f $_.DisplayName,$_.ProcessID}
    }
    
    for($i=0;$i -le processes.;$i++)
    {
        $a = processes[$i];
    }
    #>
}
#Pause
ChangeCurrentDir $MyInvocation.MyCommand.Definition

<# 反复检查指定路径的进程是否运行 #>
$json =  ReadJson "psSettings.json"
for(;;)
{
    foreach( $app in $json.CheckRunningApps){
        $exist = CheckRunning $app.processName $app.path
        if(-not $exist)
        {
            Write-Output "Starting..."
            Start-Process -FilePath $app.path
        }
        else {
            Write-Output "Existing."
        }
    }
    Start-Sleep 1
}
<# CheckRunning "notepad" "c:\notepad.exe" #>

Write-Output "not reachable"
$sum=0
<#
for($i=1;$i -le 100;$i++)
#>
for($i=1;;$i++)
{
    $sum+=$i
    Write-Output $sum
    Start-Sleep 1
}
$sum

$cmd = Get-Process -Name cmd
Write-Output $cmd.Path
DoWork 100
$date = Get-Date;
$beginTime = 2 * 60 ;
$endTime = 2 * 60 + 11;
$minuteCount = $date.hour * 60 + $date.minute

#if the time is right
if(($minuteCount -le $endTime ) -and ($minuteCount -ge $beginTime ))
{
    #send request
    $webClient = new-object System.Net.WebClient;
    $webClient.Headers.Add("user-agent","PowerShell Script");
    $webClient.Headers.Add("Referer","PowerShell Script");
    $url = "http://cms.hujiang.com/enAdmin/Handler/Listen_cms.ashx?code=listen_cms_20111130876";
    $message = $webClient.DownloadString($url);
   
    #write log
    write("Message:"+$message);
}
else
{
    write("Not Update");
}

