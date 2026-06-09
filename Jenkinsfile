pipeline {
    agent any

    environment {
        DOCKER_IMAGE = 'todoapi:latest'
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build & Test (.NET)') {
            steps {
                script {
                    if (isUnix()) {
                        sh 'dotnet build TodoApi.csproj -c Release'
                    } else {
                        bat 'dotnet build TodoApi.csproj -c Release'
                    }
                }
            }
        }

        stage('Docker Build') {
            steps {
                script {
                    if (isUnix()) {
                        sh "docker build -t ${DOCKER_IMAGE} ."
                    } else {
                        bat "docker build -t ${DOCKER_IMAGE} ."
                    }
                }
            }
        }
    }
    
    post {
        always {
            cleanWs()
        }
    }
}
