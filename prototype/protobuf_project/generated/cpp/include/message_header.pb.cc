// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: message_header.proto

#include "message_header.pb.h"

#include <algorithm>

#include <google/protobuf/io/coded_stream.h>
#include <google/protobuf/extension_set.h>
#include <google/protobuf/wire_format_lite.h>
#include <google/protobuf/descriptor.h>
#include <google/protobuf/generated_message_reflection.h>
#include <google/protobuf/reflection_ops.h>
#include <google/protobuf/wire_format.h>
// @@protoc_insertion_point(includes)
#include <google/protobuf/port_def.inc>
namespace PhysicsTelemetry {
class MessageHeaderMessageDefaultTypeInternal {
 public:
  ::PROTOBUF_NAMESPACE_ID::internal::ExplicitlyConstructed<MessageHeaderMessage> _instance;
} _MessageHeaderMessage_default_instance_;
}  // namespace PhysicsTelemetry
static void InitDefaultsscc_info_MessageHeaderMessage_message_5fheader_2eproto() {
  GOOGLE_PROTOBUF_VERIFY_VERSION;

  {
    void* ptr = &::PhysicsTelemetry::_MessageHeaderMessage_default_instance_;
    new (ptr) ::PhysicsTelemetry::MessageHeaderMessage();
    ::PROTOBUF_NAMESPACE_ID::internal::OnShutdownDestroyMessage(ptr);
  }
  ::PhysicsTelemetry::MessageHeaderMessage::InitAsDefaultInstance();
}

::PROTOBUF_NAMESPACE_ID::internal::SCCInfo<0> scc_info_MessageHeaderMessage_message_5fheader_2eproto =
    {{ATOMIC_VAR_INIT(::PROTOBUF_NAMESPACE_ID::internal::SCCInfoBase::kUninitialized), 0, 0, InitDefaultsscc_info_MessageHeaderMessage_message_5fheader_2eproto}, {}};

static ::PROTOBUF_NAMESPACE_ID::Metadata file_level_metadata_message_5fheader_2eproto[1];
static const ::PROTOBUF_NAMESPACE_ID::EnumDescriptor* file_level_enum_descriptors_message_5fheader_2eproto[1];
static constexpr ::PROTOBUF_NAMESPACE_ID::ServiceDescriptor const** file_level_service_descriptors_message_5fheader_2eproto = nullptr;

const ::PROTOBUF_NAMESPACE_ID::uint32 TableStruct_message_5fheader_2eproto::offsets[] PROTOBUF_SECTION_VARIABLE(protodesc_cold) = {
  ~0u,  // no _has_bits_
  PROTOBUF_FIELD_OFFSET(::PhysicsTelemetry::MessageHeaderMessage, _internal_metadata_),
  ~0u,  // no _extensions_
  ~0u,  // no _oneof_case_
  ~0u,  // no _weak_field_map_
  PROTOBUF_FIELD_OFFSET(::PhysicsTelemetry::MessageHeaderMessage, frameid_),
  PROTOBUF_FIELD_OFFSET(::PhysicsTelemetry::MessageHeaderMessage, messagetype_),
  PROTOBUF_FIELD_OFFSET(::PhysicsTelemetry::MessageHeaderMessage, datasize_),
};
static const ::PROTOBUF_NAMESPACE_ID::internal::MigrationSchema schemas[] PROTOBUF_SECTION_VARIABLE(protodesc_cold) = {
  { 0, -1, sizeof(::PhysicsTelemetry::MessageHeaderMessage)},
};

static ::PROTOBUF_NAMESPACE_ID::Message const * const file_default_instances[] = {
  reinterpret_cast<const ::PROTOBUF_NAMESPACE_ID::Message*>(&::PhysicsTelemetry::_MessageHeaderMessage_default_instance_),
};

const char descriptor_table_protodef_message_5fheader_2eproto[] PROTOBUF_SECTION_VARIABLE(protodesc_cold) =
  "\n\024message_header.proto\022\020PhysicsTelemetry"
  "\"\332\001\n\024MessageHeaderMessage\022\017\n\007frameId\030\001 \001"
  "(\005\022G\n\013messageType\030\002 \001(\01622.PhysicsTelemet"
  "ry.MessageHeaderMessage.MessageType\022\020\n\010d"
  "ataSize\030\003 \001(\005\"V\n\013MessageType\022\023\n\017RigidBod"
  "yUpdate\020\000\022\020\n\014ShapeCreated\020\001\022\020\n\014ShapeChan"
  "ged\020\002\022\016\n\nFrameStats\020\003B\037\252\002\034Physics.Teleme"
  "try.Serialisedb\006proto3"
  ;
static const ::PROTOBUF_NAMESPACE_ID::internal::DescriptorTable*const descriptor_table_message_5fheader_2eproto_deps[1] = {
};
static ::PROTOBUF_NAMESPACE_ID::internal::SCCInfoBase*const descriptor_table_message_5fheader_2eproto_sccs[1] = {
  &scc_info_MessageHeaderMessage_message_5fheader_2eproto.base,
};
static ::PROTOBUF_NAMESPACE_ID::internal::once_flag descriptor_table_message_5fheader_2eproto_once;
static bool descriptor_table_message_5fheader_2eproto_initialized = false;
const ::PROTOBUF_NAMESPACE_ID::internal::DescriptorTable descriptor_table_message_5fheader_2eproto = {
  &descriptor_table_message_5fheader_2eproto_initialized, descriptor_table_protodef_message_5fheader_2eproto, "message_header.proto", 302,
  &descriptor_table_message_5fheader_2eproto_once, descriptor_table_message_5fheader_2eproto_sccs, descriptor_table_message_5fheader_2eproto_deps, 1, 0,
  schemas, file_default_instances, TableStruct_message_5fheader_2eproto::offsets,
  file_level_metadata_message_5fheader_2eproto, 1, file_level_enum_descriptors_message_5fheader_2eproto, file_level_service_descriptors_message_5fheader_2eproto,
};

// Force running AddDescriptors() at dynamic initialization time.
static bool dynamic_init_dummy_message_5fheader_2eproto = (  ::PROTOBUF_NAMESPACE_ID::internal::AddDescriptors(&descriptor_table_message_5fheader_2eproto), true);
namespace PhysicsTelemetry {
const ::PROTOBUF_NAMESPACE_ID::EnumDescriptor* MessageHeaderMessage_MessageType_descriptor() {
  ::PROTOBUF_NAMESPACE_ID::internal::AssignDescriptors(&descriptor_table_message_5fheader_2eproto);
  return file_level_enum_descriptors_message_5fheader_2eproto[0];
}
bool MessageHeaderMessage_MessageType_IsValid(int value) {
  switch (value) {
    case 0:
    case 1:
    case 2:
    case 3:
      return true;
    default:
      return false;
  }
}

#if (__cplusplus < 201703) && (!defined(_MSC_VER) || _MSC_VER >= 1900)
constexpr MessageHeaderMessage_MessageType MessageHeaderMessage::RigidBodyUpdate;
constexpr MessageHeaderMessage_MessageType MessageHeaderMessage::ShapeCreated;
constexpr MessageHeaderMessage_MessageType MessageHeaderMessage::ShapeChanged;
constexpr MessageHeaderMessage_MessageType MessageHeaderMessage::FrameStats;
constexpr MessageHeaderMessage_MessageType MessageHeaderMessage::MessageType_MIN;
constexpr MessageHeaderMessage_MessageType MessageHeaderMessage::MessageType_MAX;
constexpr int MessageHeaderMessage::MessageType_ARRAYSIZE;
#endif  // (__cplusplus < 201703) && (!defined(_MSC_VER) || _MSC_VER >= 1900)

// ===================================================================

void MessageHeaderMessage::InitAsDefaultInstance() {
}
class MessageHeaderMessage::_Internal {
 public:
};

MessageHeaderMessage::MessageHeaderMessage()
  : ::PROTOBUF_NAMESPACE_ID::Message(), _internal_metadata_(nullptr) {
  SharedCtor();
  // @@protoc_insertion_point(constructor:PhysicsTelemetry.MessageHeaderMessage)
}
MessageHeaderMessage::MessageHeaderMessage(const MessageHeaderMessage& from)
  : ::PROTOBUF_NAMESPACE_ID::Message(),
      _internal_metadata_(nullptr) {
  _internal_metadata_.MergeFrom(from._internal_metadata_);
  ::memcpy(&frameid_, &from.frameid_,
    static_cast<size_t>(reinterpret_cast<char*>(&datasize_) -
    reinterpret_cast<char*>(&frameid_)) + sizeof(datasize_));
  // @@protoc_insertion_point(copy_constructor:PhysicsTelemetry.MessageHeaderMessage)
}

void MessageHeaderMessage::SharedCtor() {
  ::memset(&frameid_, 0, static_cast<size_t>(
      reinterpret_cast<char*>(&datasize_) -
      reinterpret_cast<char*>(&frameid_)) + sizeof(datasize_));
}

MessageHeaderMessage::~MessageHeaderMessage() {
  // @@protoc_insertion_point(destructor:PhysicsTelemetry.MessageHeaderMessage)
  SharedDtor();
}

void MessageHeaderMessage::SharedDtor() {
}

void MessageHeaderMessage::SetCachedSize(int size) const {
  _cached_size_.Set(size);
}
const MessageHeaderMessage& MessageHeaderMessage::default_instance() {
  ::PROTOBUF_NAMESPACE_ID::internal::InitSCC(&::scc_info_MessageHeaderMessage_message_5fheader_2eproto.base);
  return *internal_default_instance();
}


void MessageHeaderMessage::Clear() {
// @@protoc_insertion_point(message_clear_start:PhysicsTelemetry.MessageHeaderMessage)
  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  ::memset(&frameid_, 0, static_cast<size_t>(
      reinterpret_cast<char*>(&datasize_) -
      reinterpret_cast<char*>(&frameid_)) + sizeof(datasize_));
  _internal_metadata_.Clear();
}

const char* MessageHeaderMessage::_InternalParse(const char* ptr, ::PROTOBUF_NAMESPACE_ID::internal::ParseContext* ctx) {
#define CHK_(x) if (PROTOBUF_PREDICT_FALSE(!(x))) goto failure
  while (!ctx->Done(&ptr)) {
    ::PROTOBUF_NAMESPACE_ID::uint32 tag;
    ptr = ::PROTOBUF_NAMESPACE_ID::internal::ReadTag(ptr, &tag);
    CHK_(ptr);
    switch (tag >> 3) {
      // int32 frameId = 1;
      case 1:
        if (PROTOBUF_PREDICT_TRUE(static_cast<::PROTOBUF_NAMESPACE_ID::uint8>(tag) == 8)) {
          frameid_ = ::PROTOBUF_NAMESPACE_ID::internal::ReadVarint(&ptr);
          CHK_(ptr);
        } else goto handle_unusual;
        continue;
      // .PhysicsTelemetry.MessageHeaderMessage.MessageType messageType = 2;
      case 2:
        if (PROTOBUF_PREDICT_TRUE(static_cast<::PROTOBUF_NAMESPACE_ID::uint8>(tag) == 16)) {
          ::PROTOBUF_NAMESPACE_ID::uint64 val = ::PROTOBUF_NAMESPACE_ID::internal::ReadVarint(&ptr);
          CHK_(ptr);
          _internal_set_messagetype(static_cast<::PhysicsTelemetry::MessageHeaderMessage_MessageType>(val));
        } else goto handle_unusual;
        continue;
      // int32 dataSize = 3;
      case 3:
        if (PROTOBUF_PREDICT_TRUE(static_cast<::PROTOBUF_NAMESPACE_ID::uint8>(tag) == 24)) {
          datasize_ = ::PROTOBUF_NAMESPACE_ID::internal::ReadVarint(&ptr);
          CHK_(ptr);
        } else goto handle_unusual;
        continue;
      default: {
      handle_unusual:
        if ((tag & 7) == 4 || tag == 0) {
          ctx->SetLastTag(tag);
          goto success;
        }
        ptr = UnknownFieldParse(tag, &_internal_metadata_, ptr, ctx);
        CHK_(ptr != nullptr);
        continue;
      }
    }  // switch
  }  // while
success:
  return ptr;
failure:
  ptr = nullptr;
  goto success;
#undef CHK_
}

::PROTOBUF_NAMESPACE_ID::uint8* MessageHeaderMessage::_InternalSerialize(
    ::PROTOBUF_NAMESPACE_ID::uint8* target, ::PROTOBUF_NAMESPACE_ID::io::EpsCopyOutputStream* stream) const {
  // @@protoc_insertion_point(serialize_to_array_start:PhysicsTelemetry.MessageHeaderMessage)
  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  // int32 frameId = 1;
  if (this->frameid() != 0) {
    target = stream->EnsureSpace(target);
    target = ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::WriteInt32ToArray(1, this->_internal_frameid(), target);
  }

  // .PhysicsTelemetry.MessageHeaderMessage.MessageType messageType = 2;
  if (this->messagetype() != 0) {
    target = stream->EnsureSpace(target);
    target = ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::WriteEnumToArray(
      2, this->_internal_messagetype(), target);
  }

  // int32 dataSize = 3;
  if (this->datasize() != 0) {
    target = stream->EnsureSpace(target);
    target = ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::WriteInt32ToArray(3, this->_internal_datasize(), target);
  }

  if (PROTOBUF_PREDICT_FALSE(_internal_metadata_.have_unknown_fields())) {
    target = ::PROTOBUF_NAMESPACE_ID::internal::WireFormat::InternalSerializeUnknownFieldsToArray(
        _internal_metadata_.unknown_fields(), target, stream);
  }
  // @@protoc_insertion_point(serialize_to_array_end:PhysicsTelemetry.MessageHeaderMessage)
  return target;
}

size_t MessageHeaderMessage::ByteSizeLong() const {
// @@protoc_insertion_point(message_byte_size_start:PhysicsTelemetry.MessageHeaderMessage)
  size_t total_size = 0;

  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  // int32 frameId = 1;
  if (this->frameid() != 0) {
    total_size += 1 +
      ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::Int32Size(
        this->_internal_frameid());
  }

  // .PhysicsTelemetry.MessageHeaderMessage.MessageType messageType = 2;
  if (this->messagetype() != 0) {
    total_size += 1 +
      ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::EnumSize(this->_internal_messagetype());
  }

  // int32 dataSize = 3;
  if (this->datasize() != 0) {
    total_size += 1 +
      ::PROTOBUF_NAMESPACE_ID::internal::WireFormatLite::Int32Size(
        this->_internal_datasize());
  }

  if (PROTOBUF_PREDICT_FALSE(_internal_metadata_.have_unknown_fields())) {
    return ::PROTOBUF_NAMESPACE_ID::internal::ComputeUnknownFieldsSize(
        _internal_metadata_, total_size, &_cached_size_);
  }
  int cached_size = ::PROTOBUF_NAMESPACE_ID::internal::ToCachedSize(total_size);
  SetCachedSize(cached_size);
  return total_size;
}

void MessageHeaderMessage::MergeFrom(const ::PROTOBUF_NAMESPACE_ID::Message& from) {
// @@protoc_insertion_point(generalized_merge_from_start:PhysicsTelemetry.MessageHeaderMessage)
  GOOGLE_DCHECK_NE(&from, this);
  const MessageHeaderMessage* source =
      ::PROTOBUF_NAMESPACE_ID::DynamicCastToGenerated<MessageHeaderMessage>(
          &from);
  if (source == nullptr) {
  // @@protoc_insertion_point(generalized_merge_from_cast_fail:PhysicsTelemetry.MessageHeaderMessage)
    ::PROTOBUF_NAMESPACE_ID::internal::ReflectionOps::Merge(from, this);
  } else {
  // @@protoc_insertion_point(generalized_merge_from_cast_success:PhysicsTelemetry.MessageHeaderMessage)
    MergeFrom(*source);
  }
}

void MessageHeaderMessage::MergeFrom(const MessageHeaderMessage& from) {
// @@protoc_insertion_point(class_specific_merge_from_start:PhysicsTelemetry.MessageHeaderMessage)
  GOOGLE_DCHECK_NE(&from, this);
  _internal_metadata_.MergeFrom(from._internal_metadata_);
  ::PROTOBUF_NAMESPACE_ID::uint32 cached_has_bits = 0;
  (void) cached_has_bits;

  if (from.frameid() != 0) {
    _internal_set_frameid(from._internal_frameid());
  }
  if (from.messagetype() != 0) {
    _internal_set_messagetype(from._internal_messagetype());
  }
  if (from.datasize() != 0) {
    _internal_set_datasize(from._internal_datasize());
  }
}

void MessageHeaderMessage::CopyFrom(const ::PROTOBUF_NAMESPACE_ID::Message& from) {
// @@protoc_insertion_point(generalized_copy_from_start:PhysicsTelemetry.MessageHeaderMessage)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

void MessageHeaderMessage::CopyFrom(const MessageHeaderMessage& from) {
// @@protoc_insertion_point(class_specific_copy_from_start:PhysicsTelemetry.MessageHeaderMessage)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}

bool MessageHeaderMessage::IsInitialized() const {
  return true;
}

void MessageHeaderMessage::InternalSwap(MessageHeaderMessage* other) {
  using std::swap;
  _internal_metadata_.Swap(&other->_internal_metadata_);
  swap(frameid_, other->frameid_);
  swap(messagetype_, other->messagetype_);
  swap(datasize_, other->datasize_);
}

::PROTOBUF_NAMESPACE_ID::Metadata MessageHeaderMessage::GetMetadata() const {
  return GetMetadataStatic();
}


// @@protoc_insertion_point(namespace_scope)
}  // namespace PhysicsTelemetry
PROTOBUF_NAMESPACE_OPEN
template<> PROTOBUF_NOINLINE ::PhysicsTelemetry::MessageHeaderMessage* Arena::CreateMaybeMessage< ::PhysicsTelemetry::MessageHeaderMessage >(Arena* arena) {
  return Arena::CreateInternal< ::PhysicsTelemetry::MessageHeaderMessage >(arena);
}
PROTOBUF_NAMESPACE_CLOSE

// @@protoc_insertion_point(global_scope)
#include <google/protobuf/port_undef.inc>
