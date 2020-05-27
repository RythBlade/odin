// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: message_header.proto

#ifndef GOOGLE_PROTOBUF_INCLUDED_message_5fheader_2eproto
#define GOOGLE_PROTOBUF_INCLUDED_message_5fheader_2eproto

#include <limits>
#include <string>

#include <google/protobuf/port_def.inc>
#if PROTOBUF_VERSION < 3011000
#error This file was generated by a newer version of protoc which is
#error incompatible with your Protocol Buffer headers. Please update
#error your headers.
#endif
#if 3011004 < PROTOBUF_MIN_PROTOC_VERSION
#error This file was generated by an older version of protoc which is
#error incompatible with your Protocol Buffer headers. Please
#error regenerate this file with a newer version of protoc.
#endif

#include <google/protobuf/port_undef.inc>
#include <google/protobuf/io/coded_stream.h>
#include <google/protobuf/arena.h>
#include <google/protobuf/arenastring.h>
#include <google/protobuf/generated_message_table_driven.h>
#include <google/protobuf/generated_message_util.h>
#include <google/protobuf/inlined_string_field.h>
#include <google/protobuf/metadata.h>
#include <google/protobuf/generated_message_reflection.h>
#include <google/protobuf/message.h>
#include <google/protobuf/repeated_field.h>  // IWYU pragma: export
#include <google/protobuf/extension_set.h>  // IWYU pragma: export
#include <google/protobuf/generated_enum_reflection.h>
#include <google/protobuf/unknown_field_set.h>
// @@protoc_insertion_point(includes)
#include <google/protobuf/port_def.inc>
#define PROTOBUF_INTERNAL_EXPORT_message_5fheader_2eproto
PROTOBUF_NAMESPACE_OPEN
namespace internal {
class AnyMetadata;
}  // namespace internal
PROTOBUF_NAMESPACE_CLOSE

// Internal implementation detail -- do not use these members.
struct TableStruct_message_5fheader_2eproto {
  static const ::PROTOBUF_NAMESPACE_ID::internal::ParseTableField entries[]
    PROTOBUF_SECTION_VARIABLE(protodesc_cold);
  static const ::PROTOBUF_NAMESPACE_ID::internal::AuxillaryParseTableField aux[]
    PROTOBUF_SECTION_VARIABLE(protodesc_cold);
  static const ::PROTOBUF_NAMESPACE_ID::internal::ParseTable schema[1]
    PROTOBUF_SECTION_VARIABLE(protodesc_cold);
  static const ::PROTOBUF_NAMESPACE_ID::internal::FieldMetadata field_metadata[];
  static const ::PROTOBUF_NAMESPACE_ID::internal::SerializationTable serialization_table[];
  static const ::PROTOBUF_NAMESPACE_ID::uint32 offsets[];
};
extern const ::PROTOBUF_NAMESPACE_ID::internal::DescriptorTable descriptor_table_message_5fheader_2eproto;
namespace PhysicsTelemetry {
class MessageHeader;
class MessageHeaderDefaultTypeInternal;
extern MessageHeaderDefaultTypeInternal _MessageHeader_default_instance_;
}  // namespace PhysicsTelemetry
PROTOBUF_NAMESPACE_OPEN
template<> ::PhysicsTelemetry::MessageHeader* Arena::CreateMaybeMessage<::PhysicsTelemetry::MessageHeader>(Arena*);
PROTOBUF_NAMESPACE_CLOSE
namespace PhysicsTelemetry {

enum MessageHeader_MessageType : int {
  MessageHeader_MessageType_RigidBodyUpdate = 0,
  MessageHeader_MessageType_MessageHeader_MessageType_INT_MIN_SENTINEL_DO_NOT_USE_ = std::numeric_limits<::PROTOBUF_NAMESPACE_ID::int32>::min(),
  MessageHeader_MessageType_MessageHeader_MessageType_INT_MAX_SENTINEL_DO_NOT_USE_ = std::numeric_limits<::PROTOBUF_NAMESPACE_ID::int32>::max()
};
bool MessageHeader_MessageType_IsValid(int value);
constexpr MessageHeader_MessageType MessageHeader_MessageType_MessageType_MIN = MessageHeader_MessageType_RigidBodyUpdate;
constexpr MessageHeader_MessageType MessageHeader_MessageType_MessageType_MAX = MessageHeader_MessageType_RigidBodyUpdate;
constexpr int MessageHeader_MessageType_MessageType_ARRAYSIZE = MessageHeader_MessageType_MessageType_MAX + 1;

const ::PROTOBUF_NAMESPACE_ID::EnumDescriptor* MessageHeader_MessageType_descriptor();
template<typename T>
inline const std::string& MessageHeader_MessageType_Name(T enum_t_value) {
  static_assert(::std::is_same<T, MessageHeader_MessageType>::value ||
    ::std::is_integral<T>::value,
    "Incorrect type passed to function MessageHeader_MessageType_Name.");
  return ::PROTOBUF_NAMESPACE_ID::internal::NameOfEnum(
    MessageHeader_MessageType_descriptor(), enum_t_value);
}
inline bool MessageHeader_MessageType_Parse(
    const std::string& name, MessageHeader_MessageType* value) {
  return ::PROTOBUF_NAMESPACE_ID::internal::ParseNamedEnum<MessageHeader_MessageType>(
    MessageHeader_MessageType_descriptor(), name, value);
}
// ===================================================================

class MessageHeader :
    public ::PROTOBUF_NAMESPACE_ID::Message /* @@protoc_insertion_point(class_definition:PhysicsTelemetry.MessageHeader) */ {
 public:
  MessageHeader();
  virtual ~MessageHeader();

  MessageHeader(const MessageHeader& from);
  MessageHeader(MessageHeader&& from) noexcept
    : MessageHeader() {
    *this = ::std::move(from);
  }

  inline MessageHeader& operator=(const MessageHeader& from) {
    CopyFrom(from);
    return *this;
  }
  inline MessageHeader& operator=(MessageHeader&& from) noexcept {
    if (GetArenaNoVirtual() == from.GetArenaNoVirtual()) {
      if (this != &from) InternalSwap(&from);
    } else {
      CopyFrom(from);
    }
    return *this;
  }

  static const ::PROTOBUF_NAMESPACE_ID::Descriptor* descriptor() {
    return GetDescriptor();
  }
  static const ::PROTOBUF_NAMESPACE_ID::Descriptor* GetDescriptor() {
    return GetMetadataStatic().descriptor;
  }
  static const ::PROTOBUF_NAMESPACE_ID::Reflection* GetReflection() {
    return GetMetadataStatic().reflection;
  }
  static const MessageHeader& default_instance();

  static void InitAsDefaultInstance();  // FOR INTERNAL USE ONLY
  static inline const MessageHeader* internal_default_instance() {
    return reinterpret_cast<const MessageHeader*>(
               &_MessageHeader_default_instance_);
  }
  static constexpr int kIndexInFileMessages =
    0;

  friend void swap(MessageHeader& a, MessageHeader& b) {
    a.Swap(&b);
  }
  inline void Swap(MessageHeader* other) {
    if (other == this) return;
    InternalSwap(other);
  }

  // implements Message ----------------------------------------------

  inline MessageHeader* New() const final {
    return CreateMaybeMessage<MessageHeader>(nullptr);
  }

  MessageHeader* New(::PROTOBUF_NAMESPACE_ID::Arena* arena) const final {
    return CreateMaybeMessage<MessageHeader>(arena);
  }
  void CopyFrom(const ::PROTOBUF_NAMESPACE_ID::Message& from) final;
  void MergeFrom(const ::PROTOBUF_NAMESPACE_ID::Message& from) final;
  void CopyFrom(const MessageHeader& from);
  void MergeFrom(const MessageHeader& from);
  PROTOBUF_ATTRIBUTE_REINITIALIZES void Clear() final;
  bool IsInitialized() const final;

  size_t ByteSizeLong() const final;
  const char* _InternalParse(const char* ptr, ::PROTOBUF_NAMESPACE_ID::internal::ParseContext* ctx) final;
  ::PROTOBUF_NAMESPACE_ID::uint8* _InternalSerialize(
      ::PROTOBUF_NAMESPACE_ID::uint8* target, ::PROTOBUF_NAMESPACE_ID::io::EpsCopyOutputStream* stream) const final;
  int GetCachedSize() const final { return _cached_size_.Get(); }

  private:
  inline void SharedCtor();
  inline void SharedDtor();
  void SetCachedSize(int size) const final;
  void InternalSwap(MessageHeader* other);
  friend class ::PROTOBUF_NAMESPACE_ID::internal::AnyMetadata;
  static ::PROTOBUF_NAMESPACE_ID::StringPiece FullMessageName() {
    return "PhysicsTelemetry.MessageHeader";
  }
  private:
  inline ::PROTOBUF_NAMESPACE_ID::Arena* GetArenaNoVirtual() const {
    return nullptr;
  }
  inline void* MaybeArenaPtr() const {
    return nullptr;
  }
  public:

  ::PROTOBUF_NAMESPACE_ID::Metadata GetMetadata() const final;
  private:
  static ::PROTOBUF_NAMESPACE_ID::Metadata GetMetadataStatic() {
    ::PROTOBUF_NAMESPACE_ID::internal::AssignDescriptors(&::descriptor_table_message_5fheader_2eproto);
    return ::descriptor_table_message_5fheader_2eproto.file_level_metadata[kIndexInFileMessages];
  }

  public:

  // nested types ----------------------------------------------------

  typedef MessageHeader_MessageType MessageType;
  static constexpr MessageType RigidBodyUpdate =
    MessageHeader_MessageType_RigidBodyUpdate;
  static inline bool MessageType_IsValid(int value) {
    return MessageHeader_MessageType_IsValid(value);
  }
  static constexpr MessageType MessageType_MIN =
    MessageHeader_MessageType_MessageType_MIN;
  static constexpr MessageType MessageType_MAX =
    MessageHeader_MessageType_MessageType_MAX;
  static constexpr int MessageType_ARRAYSIZE =
    MessageHeader_MessageType_MessageType_ARRAYSIZE;
  static inline const ::PROTOBUF_NAMESPACE_ID::EnumDescriptor*
  MessageType_descriptor() {
    return MessageHeader_MessageType_descriptor();
  }
  template<typename T>
  static inline const std::string& MessageType_Name(T enum_t_value) {
    static_assert(::std::is_same<T, MessageType>::value ||
      ::std::is_integral<T>::value,
      "Incorrect type passed to function MessageType_Name.");
    return MessageHeader_MessageType_Name(enum_t_value);
  }
  static inline bool MessageType_Parse(const std::string& name,
      MessageType* value) {
    return MessageHeader_MessageType_Parse(name, value);
  }

  // accessors -------------------------------------------------------

  enum : int {
    kFrameIdFieldNumber = 1,
    kMessageTypeFieldNumber = 2,
    kDataSizeFieldNumber = 3,
  };
  // uint32 frameId = 1;
  void clear_frameid();
  ::PROTOBUF_NAMESPACE_ID::uint32 frameid() const;
  void set_frameid(::PROTOBUF_NAMESPACE_ID::uint32 value);
  private:
  ::PROTOBUF_NAMESPACE_ID::uint32 _internal_frameid() const;
  void _internal_set_frameid(::PROTOBUF_NAMESPACE_ID::uint32 value);
  public:

  // .PhysicsTelemetry.MessageHeader.MessageType messageType = 2;
  void clear_messagetype();
  ::PhysicsTelemetry::MessageHeader_MessageType messagetype() const;
  void set_messagetype(::PhysicsTelemetry::MessageHeader_MessageType value);
  private:
  ::PhysicsTelemetry::MessageHeader_MessageType _internal_messagetype() const;
  void _internal_set_messagetype(::PhysicsTelemetry::MessageHeader_MessageType value);
  public:

  // int32 dataSize = 3;
  void clear_datasize();
  ::PROTOBUF_NAMESPACE_ID::int32 datasize() const;
  void set_datasize(::PROTOBUF_NAMESPACE_ID::int32 value);
  private:
  ::PROTOBUF_NAMESPACE_ID::int32 _internal_datasize() const;
  void _internal_set_datasize(::PROTOBUF_NAMESPACE_ID::int32 value);
  public:

  // @@protoc_insertion_point(class_scope:PhysicsTelemetry.MessageHeader)
 private:
  class _Internal;

  ::PROTOBUF_NAMESPACE_ID::internal::InternalMetadataWithArena _internal_metadata_;
  ::PROTOBUF_NAMESPACE_ID::uint32 frameid_;
  int messagetype_;
  ::PROTOBUF_NAMESPACE_ID::int32 datasize_;
  mutable ::PROTOBUF_NAMESPACE_ID::internal::CachedSize _cached_size_;
  friend struct ::TableStruct_message_5fheader_2eproto;
};
// ===================================================================


// ===================================================================

#ifdef __GNUC__
  #pragma GCC diagnostic push
  #pragma GCC diagnostic ignored "-Wstrict-aliasing"
#endif  // __GNUC__
// MessageHeader

// uint32 frameId = 1;
inline void MessageHeader::clear_frameid() {
  frameid_ = 0u;
}
inline ::PROTOBUF_NAMESPACE_ID::uint32 MessageHeader::_internal_frameid() const {
  return frameid_;
}
inline ::PROTOBUF_NAMESPACE_ID::uint32 MessageHeader::frameid() const {
  // @@protoc_insertion_point(field_get:PhysicsTelemetry.MessageHeader.frameId)
  return _internal_frameid();
}
inline void MessageHeader::_internal_set_frameid(::PROTOBUF_NAMESPACE_ID::uint32 value) {
  
  frameid_ = value;
}
inline void MessageHeader::set_frameid(::PROTOBUF_NAMESPACE_ID::uint32 value) {
  _internal_set_frameid(value);
  // @@protoc_insertion_point(field_set:PhysicsTelemetry.MessageHeader.frameId)
}

// .PhysicsTelemetry.MessageHeader.MessageType messageType = 2;
inline void MessageHeader::clear_messagetype() {
  messagetype_ = 0;
}
inline ::PhysicsTelemetry::MessageHeader_MessageType MessageHeader::_internal_messagetype() const {
  return static_cast< ::PhysicsTelemetry::MessageHeader_MessageType >(messagetype_);
}
inline ::PhysicsTelemetry::MessageHeader_MessageType MessageHeader::messagetype() const {
  // @@protoc_insertion_point(field_get:PhysicsTelemetry.MessageHeader.messageType)
  return _internal_messagetype();
}
inline void MessageHeader::_internal_set_messagetype(::PhysicsTelemetry::MessageHeader_MessageType value) {
  
  messagetype_ = value;
}
inline void MessageHeader::set_messagetype(::PhysicsTelemetry::MessageHeader_MessageType value) {
  _internal_set_messagetype(value);
  // @@protoc_insertion_point(field_set:PhysicsTelemetry.MessageHeader.messageType)
}

// int32 dataSize = 3;
inline void MessageHeader::clear_datasize() {
  datasize_ = 0;
}
inline ::PROTOBUF_NAMESPACE_ID::int32 MessageHeader::_internal_datasize() const {
  return datasize_;
}
inline ::PROTOBUF_NAMESPACE_ID::int32 MessageHeader::datasize() const {
  // @@protoc_insertion_point(field_get:PhysicsTelemetry.MessageHeader.dataSize)
  return _internal_datasize();
}
inline void MessageHeader::_internal_set_datasize(::PROTOBUF_NAMESPACE_ID::int32 value) {
  
  datasize_ = value;
}
inline void MessageHeader::set_datasize(::PROTOBUF_NAMESPACE_ID::int32 value) {
  _internal_set_datasize(value);
  // @@protoc_insertion_point(field_set:PhysicsTelemetry.MessageHeader.dataSize)
}

#ifdef __GNUC__
  #pragma GCC diagnostic pop
#endif  // __GNUC__

// @@protoc_insertion_point(namespace_scope)

}  // namespace PhysicsTelemetry

PROTOBUF_NAMESPACE_OPEN

template <> struct is_proto_enum< ::PhysicsTelemetry::MessageHeader_MessageType> : ::std::true_type {};
template <>
inline const EnumDescriptor* GetEnumDescriptor< ::PhysicsTelemetry::MessageHeader_MessageType>() {
  return ::PhysicsTelemetry::MessageHeader_MessageType_descriptor();
}

PROTOBUF_NAMESPACE_CLOSE

// @@protoc_insertion_point(global_scope)

#include <google/protobuf/port_undef.inc>
#endif  // GOOGLE_PROTOBUF_INCLUDED_GOOGLE_PROTOBUF_INCLUDED_message_5fheader_2eproto