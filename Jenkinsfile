pipeline {
    agent any 
    triggers { // https://www.jenkins.io/doc/book/pipeline/syntax/#triggers
        cron("1 2 * * *") // https://en.wikipedia.org/wiki/Cron + https://crontab.guru/
        pollSCM("5 * * * *") // https://en.wikipedia.org/wiki/Cron + https://crontab.guru/ 
    }
    stages {
        stage("Building the API") {
            when {
                anyOf {
                    changeset "SigmaBooking.Backend/SigmaBooking.WebApi/**"
                    changeset "SigmaBooking.Backend/SigmaBooking.Domain/**"
                    changeset "SigmaBooking.Backend/SigmaBooking.MongoDB/**"
                    changeset "SigmaBooking.Backend/SigmaBooking.Core/**"
                }
            } 
            steps {
                sh "dotnet build SigmaBooking.Backend/SigmaBooking.sln"
                sh "docker-compose --env-file config/Test.env build api"
            }
        }
        stage("Building the frontend") {
            steps {
                sh "docker-compose --env-file config/Test.env build web"
                dir("SigmaBooking.Frontend") {
                    sh "npm run build"
                }
            }
        }
        stage ("Testing the API") {
            steps {
                dir("SigmaBooking.Backend/SigmaBooking.Core.Test") {
                    sh "dotnet add package coverlet.collector"
                    sh "dotnet test --collect:'XPlat Code Coverage'"
                }

                dir("SigmaBooking.Backend/SigmaBooking.Domain.Test") {
                    sh "dotnet add package coverlet.collector"
                    sh "dotnet test --collect:'XPlat Code Coverage'"
                }
            }
            post {
                success {
                    publishCoverage adapters: [coberturaAdapter("SigmaBooking.Backend/SigmaBooking.Core.Test/TestResults/*/coverage.cobertura.xml")]
                    publishCoverage adapters: [coberturaAdapter("SigmaBooking.Backend/SigmaBooking.Domain.Test/TestResults/*/coverage.cobertura.xml")]
                }
            }
        }
        stage("Clean containers") {
            steps {
                script {
                    try {
                        sh "docker-compose --env-file config/Test.env down"
                    }
                    finally { }
                }
            }
        }
        stage("Deploy API") {
            steps {
                sh "docker-compose --env-file config/Test.env up -d"
            }
        }
        stage("Push images to registry") {
            steps {
                sh "docker-compose --env-file config/Test.env push"
            }
        }
     }
}