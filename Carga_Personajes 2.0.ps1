clear-host
$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
$headers.Add("accept-language", "es-ES,es;q=0.9,en;q=0.8")
$headers.Add("authority", "localhost:5001")
$headers.Add("cache-control", "no-cache")
$headers.Add("content-type", "application/json")
$headers.Add("sec-fetch-dest", "document")
$headers.Add("sec-fetch-mode", "navigate")
$headers.Add("sec-fetch-site", "none")
$headers.Add("sec-fetch-user", "?1")
$headers.Add("upgrade-insecure-requests", "1")
$headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36")

$characters=get-content -path "..\characters.csv" |ConvertFrom-Csv -Delimiter ","
foreach($item in $characters){
    $name=$item.name.Split("(")[0].trim()

    <#$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
$headers.Add("accept-language", "es-ES,es;q=0.9,en;q=0.8")
$headers.Add("authority", "localhost:5001")
$headers.Add("cache-control", "no-cache")
$headers.Add("content-type", "application/json")
$headers.Add("sec-fetch-dest", "document")
$headers.Add("sec-fetch-mode", "navigate")
$headers.Add("sec-fetch-site", "none")
$headers.Add("sec-fetch-user", "?1")
$headers.Add("upgrade-insecure-requests", "1")
$headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36")#>


$body = "{
`n        `"name`": `"$name`",
`n        `"universe`": `"Marvel`",
`n        `"alignment`": null,
`n        `"year`": null,
`n        `"race`": null,
`n        `"status`": null,
`n        `"gender`": null,
`n        `"stats`": {
`n            `"intelligence`": null,
`n            `"strength`": null,
`n            `"speed`": null,
`n            `"durability`": null,
`n            `"power`": null,
`n            `"combat`": null,
`n            `"total`": null
`n        },
        `n       
        `n        `"power_values`":null
`n    }"

$response = Invoke-RestMethod 'https://localhost:5001/api/personajes/' -Method 'POST' -UseBasicParsing -Headers $headers -Body $body
$response# | ConvertTo-Json



}


###########################################################################
clear-host
$characters_info=get-content -path "..\characters_info.csv" |ConvertFrom-Csv -Delimiter ","
foreach($item in $characters_info){
    $name=$item.name.Split("(")[0].trim()
    $universe=$item.Publisher
    $alignment=$item.Alignment
    $race=$item.race
    $gender=$item.gender

    if($universe -eq "Marvel Comics"){
        $universe="Marvel"
    }elseif($universe -eq "DC Comics"){
        $universe="DC"
    }
    if($gender -eq "Male"){
        $gender="M"
    }elseif($gender -eq "Female"){
        $gender="F"
    }

    <#$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
    $headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
    $headers.Add("accept-language", "es-ES,es;q=0.9,en;q=0.8")
    $headers.Add("authority", "localhost:5001")
    $headers.Add("cache-control", "no-cache")
    $headers.Add("content-type", "application/json")
    $headers.Add("sec-fetch-dest", "document")
    $headers.Add("sec-fetch-mode", "navigate")
    $headers.Add("sec-fetch-site", "none")
    $headers.Add("sec-fetch-user", "?1")
    $headers.Add("upgrade-insecure-requests", "1")
    $headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36")#>
    

    $body = ""

    $response = Invoke-RestMethod "https://localhost:5001/api/personajes/$name" -Method 'GET' -UseBasicParsing -Headers $headers
    #$response | ConvertTo-Json
    
    #si no lo encuentra lo crea
    if($response.Length -eq 0){
       
        

        $body = "{
        `n        `"name`": `"$name`",
        `n        `"universe`": `"$universe`",
        `n        `"alignment`": `"$alignment`",
        `n        `"year`": null,
        `n        `"race`": `"$race`",
        `n        `"status`": null,
        `n        `"gender`": `"$gender`",
        `n        `"stats`": {
        `n            `"intelligence`": null,
        `n            `"strength`": null,
        `n            `"speed`": null,
        `n            `"durability`": null,
        `n            `"power`": null,
        `n            `"combat`": null,
        `n            `"total`": null
        `n        },
        `n       
        `n        `"power_values`":null
        `n    }"

        $response = Invoke-RestMethod 'https://localhost:5001/api/personajes/' -Method 'POST' -UseBasicParsing -Headers $headers -Body $body
        $response# | ConvertTo-Json
    }
    else{#si lo encuentra lo modifica
        $id=$response._id

        <#$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
        $headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
        $headers.Add("accept-language", "es-ES,es;q=0.9,en;q=0.8")
        $headers.Add("authority", "localhost:5001")
        $headers.Add("cache-control", "no-cache")
        $headers.Add("content-type", "application/json")
        $headers.Add("sec-fetch-dest", "document")
        $headers.Add("sec-fetch-mode", "navigate")
        $headers.Add("sec-fetch-site", "none")
        $headers.Add("sec-fetch-user", "?1")
        $headers.Add("upgrade-insecure-requests", "1")
        $headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36")#>

        $body = "{
        `n	  `"_id`": `"$id`",
        `n    `"name`": `"$name`",
        `n    `"alignment`": `"$alignment`",
        `n	  `"race`": `"$race`",
        `n	  `"gender`": `"$gender`",
                `n        `"power_values`":null
        `n}"

        $response = Invoke-RestMethod 'https://localhost:5001/api/personajes/' -UseBasicParsing -Method 'PUT' -Headers $headers -Body $body
        $response# | ConvertTo-Json
    }
    
}


############################################################################


clear-host
$marvel_dc_characters=get-content -path "..\marvel_dc_characters.csv" |ConvertFrom-Csv -Delimiter ","


foreach($item in $marvel_dc_characters){
    $name=$item.name.Split("(")[0].trim()
    $alignment=$item.Alignment
    $gender=$item.gender
    $status=$item.status
    $year=$item.year
    $universe=$item.universe
    
    if($gender -eq "Male"){
        $gender="M"
    }elseif($gender -eq "Female"){
        $gender="F"
    }

    

   <# $headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
    $headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
    $headers.Add("accept-language", "es-ES,es;q=0.9,en;q=0.8")
    $headers.Add("authority", "localhost:5001")
    $headers.Add("cache-control", "no-cache")
    $headers.Add("content-type", "application/json")
    $headers.Add("sec-fetch-dest", "document")
    $headers.Add("sec-fetch-mode", "navigate")
    $headers.Add("sec-fetch-site", "none")
    $headers.Add("sec-fetch-user", "?1")
    $headers.Add("upgrade-insecure-requests", "1")
    $headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36")#>
    

    $body = ""

    $response = Invoke-RestMethod "https://localhost:5001/api/personajes/$name" -Method 'GET' -UseBasicParsing -Headers $headers
    #$response | ConvertTo-Json
    
    #si no lo encuentra lo crea
    if($response.Length -eq 0){
       
        

        $body = "{
        `n        `"name`": `"$name`",
        `n        `"universe`": `"$universe`",
        `n        `"alignment`": `"$alignment`",
        `n        `"year`": $year,
        `n        `"race`": null,
        `n        `"status`": `"$status`",
        `n        `"gender`": `"$gender`",
        `n        `"stats`": {
        `n            `"intelligence`": null,
        `n            `"strength`": null,
        `n            `"speed`": null,
        `n            `"durability`": null,
        `n            `"power`": null,
        `n            `"combat`": null,
        `n            `"total`": null
        `n        },
        `n       
        `n        `"power_values`":null
        `n    }"

        $response = Invoke-RestMethod 'https://localhost:5001/api/personajes/' -Method 'POST' -UseBasicParsing -Headers $headers -Body $body
        $response# | ConvertTo-Json
    }
    else{#si lo encuentra lo modifica
        $id=$response._id

        <#$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
        $headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
        $headers.Add("accept-language", "es-ES,es;q=0.9,en;q=0.8")
        $headers.Add("authority", "localhost:5001")
        $headers.Add("cache-control", "no-cache")
        $headers.Add("content-type", "application/json")
        $headers.Add("sec-fetch-dest", "document")
        $headers.Add("sec-fetch-mode", "navigate")
        $headers.Add("sec-fetch-site", "none")
        $headers.Add("sec-fetch-user", "?1")
        $headers.Add("upgrade-insecure-requests", "1")
        $headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36")#>

        $body = "{
        `n	  `"_id`": `"$id`",
        `n        `"name`": `"$name`",
        `n        `"universe`": `"$universe`",
        `n        `"alignment`": `"$alignment`",
        `n        `"status`": `"$status`",
        `n        `"gender`": `"$gender`",
        `n        `"year`": $year,
                `n        `"power_values`":null
        `n}"

        $response = Invoke-RestMethod 'https://localhost:5001/api/personajes/' -UseBasicParsing -Method 'PUT' -Headers $headers -Body $body
        $response# | ConvertTo-Json
    }
    
}





###############################################################################
clear-host
$characters_stats=get-content -path "..\characters_stats.csv" |ConvertFrom-Csv -Delimiter ","


foreach($item in $characters_stats){
    $name=$item.name.Split("(")[0].trim()
    $alignment=$item.Alignment
    $intelligence=$item.intelligence
    $strength=$item.strength
    $speed=$item.speed
    $durability=$item.durability
    $power=$item.power
    $combat=$item.combat
    $total=$item.total

    

    <#$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
    $headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
    $headers.Add("accept-language", "es-ES,es;q=0.9,en;q=0.8")
    $headers.Add("authority", "localhost:5001")
    $headers.Add("cache-control", "no-cache")
    $headers.Add("content-type", "application/json")
    $headers.Add("sec-fetch-dest", "document")
    $headers.Add("sec-fetch-mode", "navigate")
    $headers.Add("sec-fetch-site", "none")
    $headers.Add("sec-fetch-user", "?1")
    $headers.Add("upgrade-insecure-requests", "1")
    $headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36")#>
    

    $body = ""

    $response = Invoke-RestMethod "https://localhost:5001/api/personajes/$name" -Method 'GET' -UseBasicParsing -Headers $headers
    #$response | ConvertTo-Json
    
    #si no lo encuentra lo crea
    if($response.Length -eq 0){
        

        $body = "{
        `n        `"name`": `"$name`",
        `n        `"universe`": null,
        `n        `"alignment`": `"$alignment`",
        `n        `"year`": null,
        `n        `"race`": null,
        `n        `"status`": null,
        `n        `"gender`": null,
        `n        `"stats`": {
        `n            `"intelligence`": `"$intelligence`",
        `n            `"strength`": `"$strength`",
        `n            `"speed`": `"$speed`",
        `n            `"durability`": `"$durability`",
        `n            `"power`": `"$power`",
        `n            `"combat`": `"$combat`",
        `n            `"total`": `"$total`"
        `n        },
        `n       
        `n        `"power_values`":null
        `n    }"

        $response = Invoke-RestMethod 'https://localhost:5001/api/personajes/' -Method 'POST' -UseBasicParsing -Headers $headers -Body $body
        $response# | ConvertTo-Json
    }
    else{#si lo encuentra lo modifica
        $id=$response._id

        <#$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
        $headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
        $headers.Add("accept-language", "es-ES,es;q=0.9,en;q=0.8")
        $headers.Add("authority", "localhost:5001")
        $headers.Add("cache-control", "no-cache")
        $headers.Add("content-type", "application/json")
        $headers.Add("sec-fetch-dest", "document")
        $headers.Add("sec-fetch-mode", "navigate")
        $headers.Add("sec-fetch-site", "none")
        $headers.Add("sec-fetch-user", "?1")
        $headers.Add("upgrade-insecure-requests", "1")
        $headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36")#>

        $body = "{
        `n	  `"_id`": `"$id`",
        `n        `"name`": `"$name`",
        `n        `"stats`": {
        `n            `"intelligence`": $intelligence,
        `n            `"strength`": $strength,
        `n            `"speed`": $speed,
        `n            `"durability`": $durability,
        `n            `"power`": $power,
        `n            `"combat`": $combat,
        `n            `"total`": $total
        `n        },
                `n        `"power_values`":null
        `n}"

        $response = Invoke-RestMethod 'https://localhost:5001/api/personajes/' -UseBasicParsing -Method 'PUT' -Headers $headers -Body $body
        $response #| ConvertTo-Json
    }
    
}






###############################################################################
clear-host
$power_matrix=get-content -path "..\superheroes_power_matrix.csv" |ConvertFrom-Csv -Delimiter ","
$powers=$power_matrix[0].psobject.properties.name
foreach($item in $power_matrix){
    $c=0
    $sp_body=""
    $name=$item.Name.Split("(")[0].trim()
    foreach($pow in $powers){
        if($item.$pow -match "TRUE" -and $c -ne 0){
            $sp_body+="{
`"power_name`": `"$pow`"
},"
        }
        $c++

    }
    $sp_body=$sp_body.Substring(0,$sp_body.Length-1)
    

   <# $headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
    $headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
    $headers.Add("accept-language", "es-ES,es;q=0.9,en;q=0.8")
    $headers.Add("authority", "localhost:5001")
    $headers.Add("cache-control", "no-cache")
    $headers.Add("content-type", "application/json")
    $headers.Add("sec-fetch-dest", "document")
    $headers.Add("sec-fetch-mode", "navigate")
    $headers.Add("sec-fetch-site", "none")
    $headers.Add("sec-fetch-user", "?1")
    $headers.Add("upgrade-insecure-requests", "1")
    $headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36")#>
    

    $body = ""

    $response = Invoke-RestMethod "https://localhost:5001/api/personajes/$name" -Method 'GET' -UseBasicParsing -Headers $headers

    if($response.Length -eq 0){

        

        $body = "{
        `n        `"name`": `"$name`",
        `n       
        `n        `"power_values`":[$sp_body]
        `n    }"

        $response = Invoke-RestMethod 'https://localhost:5001/api/personajes/' -Method 'POST' -UseBasicParsing -Headers $headers -Body $body
        $response# | ConvertTo-Json
    }
    else{#si lo encuentra lo modifica
        $id=$response._id

        <#$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
        $headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9")
        $headers.Add("accept-language", "es-ES,es;q=0.9,en;q=0.8")
        $headers.Add("authority", "localhost:5001")
        $headers.Add("cache-control", "no-cache")
        $headers.Add("content-type", "application/json")
        $headers.Add("sec-fetch-dest", "document")
        $headers.Add("sec-fetch-mode", "navigate")
        $headers.Add("sec-fetch-site", "none")
        $headers.Add("sec-fetch-user", "?1")
        $headers.Add("upgrade-insecure-requests", "1")
        $headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.122 Safari/537.36")#>

        $body = "{
        `n	  `"_id`": `"$id`",
        `n       
        `n        `"power_values`":[$sp_body]
        `n    }"

        $response = Invoke-RestMethod 'https://localhost:5001/api/personajes/' -UseBasicParsing -Method 'PUT' -Headers $headers -Body $body
        $response# | ConvertTo-Json
    }
}

