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

        stage('Deploy (Run Container)') {
            steps {
                script {
                    if (isUnix()) {
                        sh "docker stop todo-backend || true"
                        sh "docker rm todo-backend || true"
                        sh "docker run -d --name todo-backend -p 5064:8080 ${DOCKER_IMAGE}"
                    } else {
                        bat "docker stop todo-backend 2>NUL || rem"
                        bat "docker rm todo-backend 2>NUL || rem"
                        bat "docker run -d --name todo-backend -p 5064:8080 ${DOCKER_IMAGE}"
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
