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
                    changeset "SigmaBooking.Backend/SigmaBooking.DB/**"
                    changeset "SigmaBooking.Backend/SigmaBooking.Core/**"
                    changeset "SigmaBooking.Backend/Security/**"
                }
            } 
            steps {
                sh "dotnet build SigmaBooking.Backend/SigmaBooking.sln"
            }
        }
     }
}