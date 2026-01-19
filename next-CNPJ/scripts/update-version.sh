#!/bin/bash
# Script to update version in .csproj

set -e

VERSION="$1"
CSPROJ_FILE="next-CNPJ/next-CNPJ.csproj"

if [ -z "$VERSION" ]; then
    echo "Usage: $0 <version>"
    exit 1
fi

# Update version in .csproj
if [[ "$OSTYPE" == "darwin"* ]]; then
    # macOS
    sed -i '' "s/<Version>.*<\/Version>/<Version>${VERSION}<\/Version>/" "$CSPROJ_FILE"
else
    # Linux
    sed -i "s/<Version>.*<\/Version>/<Version>${VERSION}<\/Version>/" "$CSPROJ_FILE"
fi

echo "Updated version to ${VERSION} in ${CSPROJ_FILE}"
