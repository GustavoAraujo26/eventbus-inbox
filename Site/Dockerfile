FROM node:latest

WORKDIR /eventbus-inbox-site
ENV PATH /eventbus-inbox-site/node_modules/.bin:$PATH

COPY package.json ./
COPY .env ./

RUN npm install --no-optional && npm cache clean --force

COPY . ./

EXPOSE 7500
CMD ["npm", "start"]