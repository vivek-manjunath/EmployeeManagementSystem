pipeline {    
    agent any

    stages {
        stage('Build'){
            steps{
                sh(script: "dotnet build EmployeeManagementSystem.API/EmployeeManagementSystem/EmployeeManagementSystem.csproj --configuration Release")
                }
            }
            stage('Test'){
            steps{
                sh(script: "dotnet test EmployeeManagementSystem.API/EmployeeManagementSystem.Tests/EmployeeManagementSystem.Tests.csproj")
                }
            }
    }
}