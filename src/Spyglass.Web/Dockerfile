# Stage 0
FROM node:9 as node
WORKDIR /app
COPY package.json yarn.lock /app/
RUN yarn
COPY ./ /app/
ARG env=prod
RUN npm run build -- --prod --environment $env

# Stage 1
FROM nginx
LABEL author="Protonyx"
COPY --from=node /app/dist/ /usr/share/nginx/html
COPY ./config/nginx-custom.conf /etc/nginx/conf.d/default.conf
