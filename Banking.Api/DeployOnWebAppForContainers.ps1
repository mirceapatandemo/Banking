
az login

#ReourceGroup
$resourceGroup = "bankingappservice"
$location = "westeurope"
az group create -l $location -n $resourceGroup

#AppService Plan
$planName ="bankingappservice"
az appservice plan create -n $planName -g $resourceGroup -l $location --is-linux --sku B1

#Create SqlService
$sqlServerName = "sql-bankingdb"
$adminUser = "bankingadmin"
$adminPassword = "MeR!v23090xmkbNN"  #xxxxxxxxxxx
az sql server create -g $resourceGroup -n $sqlServerName --admin-user $adminUser --admin-password $adminPassword -l $location --ssl-enforcement Disabled --sku-name B_Gen5_1 --version 5.7
az sql server firewall-rule create -g $resourceGroup --server $sqlServerName --name AllowAppService --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0

#Create WebApp
$appName="banking-mirpat"
$dockerRepo = "mirceapatan/banking:multi"  #From DockerHub
az webapp create -n $appName -g $resourceGroup --plan $planName -i $dockerRepo

#Update ConnectionString
$bankingDbHost = (az sql server show -g $resourceGroup -n $mySqlServerName --query "fullyQualifiedDomainName" -o tsv)
az webapp config connection-string set -g $resourceGroup -n $appName -t SQLAzure --settings BankingDBConnectionString="Server=$bankingDbHost;Database=BankingDB;User Id=$adminUser;Password=$adminPassword;"

#
#	https://banking-mirpat.azurewebsites.net/api/accounts
#	https://banking-mirpat.azurewebsites.net/api/transactions/report?resourceId=450ffbb8-9f11-4ec6-a1e1-df48aefc82ef


