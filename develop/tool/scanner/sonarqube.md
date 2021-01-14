# Sonarqube

- [網站](https://www.sonarqube.org/developer-edition/?gads_campaign=SonarQube&gads_ad_group=SonarQube&gads_keyword=sonarqube&gclid=Cj0KCQiA0fr_BRDaARIsAABw4Eskk6Zb7-sNX0HarDMkKRl2lUXLR0qcQF1-GfaTfCZEYlo3dZC2BBcaAnDeEALw_wcB)

## install the server
- installing sonarqube from the docker image
- [documentation](https://docs.sonarqube.org/latest/setup/install-server/)

## 下載Scanners
SonarScanner for .NET
- [msbuild](https://docs.sonarqube.org/latest/analysis/scan/sonarscanner-for-msbuild/)

1. 確認是否有安裝java
2. 環境變數是設置$path
   - 與window search 輸入env
   - 設定環境變數path
4. 修改$install_directory/sonarqube.analsis.xml，設置url與userId,password
5. 安裝dotnet tool install --global dotnet-sonarcanner --version x.x.x
6. 於專案或方案檔路徑輸入
````powershell
dotnet sonarscanner begin /k:"produect key" /d:sonar.login="token"
dotnet build
dotnet sonarscanner end /d:sonar.login="token"
````
7. 確認專案或方案可正常建置，不然無法產生結果
