FROM node:alpine as builder

WORKDIR /app
COPY package.json package-lock.json ./
ENV CI=1
RUN npm ci

COPY . .
RUN npm run build -- --prod --output-path=/dist

FROM nginx:alpine

RUN rm -rf /usr/share/nginx/html/*
COPY --from=builder /dist /usr/share/nginx/html

COPY ./nginx/nginx.conf /etc/nginx/nginx.conf
COPY ./nginx/gzip.conf /etc/nginx/gzip.conf

ENTRYPOINT ["nginx", "-g", "daemon off;"]