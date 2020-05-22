Clear-Host
$comics = get-content -Path '..\comics.csv' -Encoding UTF8| ConvertFrom-Csv -Delimiter ',' 

$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
$headers.Add("accept-language", "es-ES,es;q=0.9,en;q=0.8")
$headers.Add("authority", "localhost:44350")
$headers.Add("cache-control", "no-cache")
$headers.Add("content-type", "application/json")
$headers.Add("sec-fetch-dest", "document")
$headers.Add("sec-fetch-mode", "navigate")
$headers.Add("sec-fetch-site", "none")
$headers.Add("sec-fetch-user", "?1")
$headers.Add("upgrade-insecure-requests", "1")
$headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36")

$errores=""
foreach($item in $comics){
    try{
    $name=$item.title.Split('(')[0].trim()
    $year=0
    $issue=$item.issueNumber
    try{
        if($item.title.Split('(')[1].split(')')[0] -match "^[\d\.]+$"){
            $year=$item.title.Split('(')[1].split(')')[0]
        }
        else{
            $year=$item.title.Split('(')[2].split(')')[0]
        }
    }catch{
        $year=0
    }
    $id=""
    for($i = $item.comicID.Length+1 ;$i -le 24;$i++){
        $id+="0"
    }
    $id+=$item.comicID
    $body = "{
    `n	`"_id`": `"$id`",
    `n    `"title`": `"$name`",
    `n    `"issue`": $issue,
    `n    `"year`": $year,
    `n    `"characters`": []
    `n}"
    
    $response = Invoke-RestMethod 'https://localhost:44350/api/marvelcomics' -UseBasicParsing -Method 'POST' -Headers $headers -Body $body
    "Añadido: " + $response
    }catch{
        $errores+= "No pudo ser añadido: "+$name
        
    }
    
}


################################################

$relations = get-content -Path '..\charactersToComics.csv' -Encoding UTF8| ConvertFrom-Csv -Delimiter ',' 
$characters = get-content -Path '..\characters.csv' -Encoding UTF8| ConvertFrom-Csv -Delimiter ','
foreach($item in $relations){
    try{
        $charname=($characters | Where-Object {$_.characterid -eq $item.characterID}).name
        $name=$charname.Split("(")[0].trim()
        $response = Invoke-RestMethod "https://localhost:44350/api/personajes/$name" -Method 'GET' -UseBasicParsing -Headers $headers
        $idchar=$response._id

        $id=""
        for($i = $item.comicID.Length+1 ;$i -le 24;$i++){
            $id+="0"
        }
        $id+=$item.comicID


        $body = "{
        `n        `"_id`": `"$id`",
        `n        `"character`": `"$name`"
        `n}"

        $response = Invoke-RestMethod 'https://localhost:44350/api/marvelcomics/' -UseBasicParsing -Method 'PUT' -Headers $headers -Body $body
        ($response | ConvertTo-Json)+" modificado"
    }catch{
        echo error
    }
}