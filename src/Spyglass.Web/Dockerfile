FROM node:6

RUN mkdir -p /usr/src/app

WORKDIR /usr/src/app

COPY ./dist /usr/src/app

EXPOSE 4200

CMD [ "npm", "start" ]
