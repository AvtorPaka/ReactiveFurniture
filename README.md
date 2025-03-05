# Reactive Furniture

**Mock of furniture shop with session based authentication**

## Deployment guidance

### 0. Prerequisties

**Docker and docker-compose installed on the system.**

### 1. Download sources

```sh
git clone git@github.com:AvtorPaka/ReactiveFurniture.git
```

### 2. Create and fill .env file as it shown in [template](https://github.com/AvtorPaka/ReactiveFurniture/blob/master/docker-compose/.env.template)

```sh
cd ReactiveFurniture/docker-compose/ && touch .env &&
echo "PG_DB=react-furniture
PG_PSWD=12345
PG_USER=postgres

BUILD_ARCH=<Your processor architecture>
BUILD_PLATFORM=<Your system os>

API_PORT=<Exposed port for api>
VITE_API_BASE_URL=<Base url for api + exposed port>
CLIENT_PORT=<Exposed port for client>" > .env
```

### 2.1 .env arguments

| **Argument** | **Description** | **Example** |
| -------------- | -------------- | --------------|
|   **BUILD_ARCH**  | Your processor architecture | arm64 |
| **BUILD_PLATFORM** | Your system os | linux/arm64 |
| **API_PORT** | Exposed port for api | 7179 | 
| **VITE_API_BASE_URL** | Base url for api + exposed port | http://localhost:7179|
| **CLIENT_PORT** | Exposed port for client | 3007 |


### 3. Deploy

```sh
cd ReactiveFurniture/docker-compose/ && docker compose up -d
```
