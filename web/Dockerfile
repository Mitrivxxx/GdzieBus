FROM oven/bun:1 AS build
WORKDIR /app

COPY package.json bun.lock* ./
RUN bun install --frozen-lockfile

COPY . .

ARG VITE_GOOGLE_MAPS_API_KEY
ENV VITE_GOOGLE_MAPS_API_KEY=$VITE_GOOGLE_MAPS_API_KEY

RUN bun run build

# Etap produkcyjny
FROM nginx:1.27-alpine
WORKDIR /usr/share/nginx/html
 
# Usuwamy domyślną konfigurację Nginx oraz domyślne pliki statyczne
RUN rm /etc/nginx/conf.d/default.conf && rm -rf /usr/share/nginx/html/*

# Kopiujemy naszą konfigurację Nginx
COPY nginx.conf /etc/nginx/conf.d/default.conf

# Kopiujemy zbudowane pliki
COPY --from=build /app/dist .

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]

