# Deployment

## Overview
The application is deployed at: 
https://calculator.drilind.com/swagger/index.html

## Infrastructure
- Hosted on a local server using Docker containerization
- Cloudflare acts as a reverse proxy for security and performance optimization
- Utilizes a Docker cluster for container orchestration

## CI/CD Pipeline
- Implemented using GitHub Actions
- Automatic deployment triggers:
  - On every push to the main branch
  - Changes are automatically deployed to production

## Deployment Process
1. Code is pushed to the main branch
2. GitHub Actions workflow is triggered
3. Docker container is built with the latest changes
4. Container is deployed to the Docker cluster picked up by watchtower for automatic updates
