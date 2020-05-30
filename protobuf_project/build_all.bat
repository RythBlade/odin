set PROTOPATH="%cd%\proto"
set CPP_OUT_PATH="%cd%\generated\cpp\include"
set CSHARP_OUT_PATH="%cd%\generated\csharp"

@echo ---- Starting protobuf ----

protobufCompiler\protoc.exe --proto_path=%PROTOPATH% --proto_path="%cd%\protobufCompiler" --cpp_out=%CPP_OUT_PATH% --csharp_out=%CSHARP_OUT_PATH% "%PROTOPATH%\rigid_body.proto"

@echo ---------------------------

protobufCompiler\protoc.exe --proto_path=%PROTOPATH% --proto_path="%cd%\protobufCompiler" --cpp_out=%CPP_OUT_PATH% --csharp_out=%CSHARP_OUT_PATH% "%PROTOPATH%\base_types.proto"

@echo ---------------------------

protobufCompiler\protoc.exe --proto_path=%PROTOPATH% --proto_path="%cd%\protobufCompiler" --cpp_out=%CPP_OUT_PATH% --csharp_out=%CSHARP_OUT_PATH% "%PROTOPATH%\message_header.proto"

@echo ---------------------------

protobufCompiler\protoc.exe --proto_path=%PROTOPATH% --proto_path="%cd%\protobufCompiler" --cpp_out=%CPP_OUT_PATH% --csharp_out=%CSHARP_OUT_PATH% "%PROTOPATH%\shapes.proto"

@echo ---------------------------

protobufCompiler\protoc.exe --proto_path=%PROTOPATH% --proto_path="%cd%\protobufCompiler" --cpp_out=%CPP_OUT_PATH% --csharp_out=%CSHARP_OUT_PATH% "%PROTOPATH%\telemetry_file.proto"

@echo ---------------------------
@echo ---- Protobuf Complete ----