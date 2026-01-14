# Use the official Keycloak Quarkus image
FROM quay.io/keycloak/keycloak:25.0.2

# Set working directory
WORKDIR /opt/keycloak

# Copy the plugin JAR into providers/
COPY activation-code-action-1.0-SNAPSHOT.jar /opt/keycloak/providers/

# Optional: copy custom theme if you have one
# COPY theme /opt/keycloak/themes/

# Build Keycloak to pick up the plugin and theme
RUN /opt/keycloak/bin/kc.sh build

# Default entrypoint
ENTRYPOINT ["/opt/keycloak/bin/kc.sh", "start-dev", "--http-port=8080"]