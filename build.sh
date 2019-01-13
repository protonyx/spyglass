#!/usr/bin/env bash
##########################################################################
# This is the Cake bootstrapper script for Linux and OS X.
# This file was downloaded from https://github.com/cake-build/resources
# Feel free to change this file to fit your needs.
##########################################################################

# Define directories.
SCRIPT_DIR=$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )
TOOLS_DIR=$SCRIPT_DIR/tools
TOOLS_PROJ=$TOOLS_DIR/tools.csproj
CAKE_VERSION=0.32.1
CAKE_EXE=$TOOLS_DIR/dotnet-cake
CAKE_PATH=$TOOLS_DIR/.store/cake.tool/$CAKE_VERSION


# Make sure the tools folder exist.
if [ ! -d "$TOOLS_DIR" ]; then
  mkdir "$TOOLS_DIR"
fi

###########################################################################
# INSTALL CAKE
###########################################################################

CAKE_INSTALLED_VERSION=$(dotnet-cake --version 2>&1)

if [ "$CAKE_VERSION" != "$CAKE_INSTALLED_VERSION" ]; then
    if [ ! -f "$CAKE_EXE" ] || [ ! -d "$CAKE_PATH" ]; then
        if [ -f "$CAKE_EXE" ]; then
            dotnet tool uninstall --tool-path $TOOLS_DIR Cake.Tool
        fi

        echo "Installing Cake $CAKE_VERSION..."
        dotnet tool install --tool-path $TOOLS_DIR --version $CAKE_VERSION Cake.Tool
        if [ $? -ne 0 ]; then
            echo "An error occured while installing Cake."
            exit 1
        fi
    fi

    # Make sure that Cake has been installed.
    if [ ! -f "$CAKE_EXE" ]; then
        echo "Could not find Cake.exe at '$CAKE_EXE'."
        exit 1
    fi
else
    CAKE_EXE="dotnet-cake"
fi

###########################################################################
# RUN BUILD SCRIPT
###########################################################################

# Start Cake
(exec "$CAKE_EXE" build.cake --bootstrap) && (exec "$CAKE_EXE" build.cake "$@")
