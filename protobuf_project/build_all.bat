protobufCompiler\protoc.exe --proto_path="%cd%\proto" --proto_path="%cd%\protobufCompiler" --cpp_out="%cd%\generated\cpp\include" --csharp_out="%cd%\generated\csharp" "%cd%\proto\rigid_body.proto"

protobufCompiler\protoc.exe --proto_path="%cd%\proto" --proto_path="%cd%\protobufCompiler" --cpp_out="%cd%\generated\cpp\include" --csharp_out="%cd%\generated\csharp" "%cd%\proto\base_types.proto"

protobufCompiler\protoc.exe --proto_path="%cd%\proto" --proto_path="%cd%\protobufCompiler" --cpp_out="%cd%\generated\cpp\include" --csharp_out="%cd%\generated\csharp" "%cd%\proto\message_header.proto"

protobufCompiler\protoc.exe --proto_path="%cd%\proto" --proto_path="%cd%\protobufCompiler" --cpp_out="%cd%\generated\cpp\include" --csharp_out="%cd%\generated\csharp" "%cd%\proto\shapes.proto"