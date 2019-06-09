# Bridgeman

A simple Spring Boot backend with an Angular frontend.

## Project Prerequisites

Bridgeman relies on:

  - Java
  - Maven
  - Node
  - NPM or Yarn
  - Angular (@angular-cli)

## Development and Building

### IDE Tooling

Technically, all you need is a flat text editor to develop Bridgeman.  That
said, most people will be more productive with an IDE.  Both IDEA and Visual
Studio Code have been known to work well, but other IDEs might work too.

### Clone repo

``` bash
# clone the repo
$ git clone https://github.com/mpatkisson/bridgeman
```

### API Development and Build

``` bash
# Navigate to the "api" directory
$ cd bridgeman/src/api

# Serve at localhost:8080
$ mvn spring-boot:run

# Build for production
$ mvn package
```

### UI Development and Build

``` bash
# Navigate to the "ui" directory
$ cd bridgeman/src/ui

# Install UI dependencies
$ npm install

# Serve with hot reload at localhost:4200
$ ng serve

# Build for production with minification
$ ng build
```

## Contributions

Contributions are welcome.  Please see our [Contribution Guidelines](CONTRIBUTING.md)
for general rules on contributing to the project.
