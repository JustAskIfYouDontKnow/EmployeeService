FROM node:18-alpine

WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .

RUN npm run build --prod
RUN npm install -g serve

CMD ["serve", "-s", "dist/client-app/browser"]

EXPOSE 3000
