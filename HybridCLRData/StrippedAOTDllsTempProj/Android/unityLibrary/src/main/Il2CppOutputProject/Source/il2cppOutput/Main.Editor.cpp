#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>


template <typename T1>
struct InterfaceActionInvoker1
{
	typedef void (*Action)(void*, T1, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
template <typename T1, typename T2, typename T3>
struct InterfaceActionInvoker3
{
	typedef void (*Action)(void*, T1, T2, T3, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1, T2 p2, T3 p3)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, p2, p3, invokeData.method);
	}
};
template <typename R>
struct InterfaceFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1>
struct InterfaceFuncInvoker1
{
	typedef R (*Func)(void*, T1, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		return ((Func)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};

struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87;
struct IStateMachineRunnerPromise_1_tDE5A582A56CD943C240DEE90935B51C23578A18B;
struct IStateMachineRunnerPromise_1_t02D229AEDCE1B483DEB9B563AB65E91B66BDCD24;
struct IUniTaskSource_1_tDC639FD163E4613AFE1D3F2749CA5B177DC702F5;
struct IUniTaskSource_1_t1FB3137F5BE9A021EBCBD1CD2967E3240FC5375C;
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D;
struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD;
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
struct IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832;
struct StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF;
struct StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248;
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07;
struct AsyncCallback_t7FEF460CBDCFB9C5FA2EF776984778B9A4145F4C;
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
struct Exception_t;
struct IAsyncResult_t7B9B5A0ECB35DCEC31B8A8122C37D687369253B5;
struct IAsyncStateMachine_t0680C7F905C553076B552D5A1A6E39E2F0F36AA2;
struct IDictionary_t6D03155AF1FA9083817AA5B6AD7DEEACC26AB220;
struct MethodInfo_t;
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
struct SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6;
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A;
struct String_t;
struct UnitySourceGeneratedAssemblyMonoScriptTypes_v1_t5EB3EA56A025AD174972132A644696526652F878;
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
struct YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879;

IL2CPP_EXTERN_C RuntimeClass* Application_tDB03BE91CDF0ACA614A5E0B67CFB77C44EB19B21_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AwaiterActions_t5D05CAC006FDEBCF6B65E2B9224BC4B44783BBE5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Exception_t_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* UniTask_t8E1453C1D8424B1FC22B0E51B017D3B028E17270_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeField* U3CPrivateImplementationDetailsU3E_tCFC97FDA94A6A475F91ECDEA9CCE1D657CE6E51D____9619B8502CE689EE4D94200200B950E684692769E7FF60F546E418310BFB325E_FieldInfo_var;
IL2CPP_EXTERN_C RuntimeField* U3CPrivateImplementationDetailsU3E_tCFC97FDA94A6A475F91ECDEA9CCE1D657CE6E51D____984E4CB0C0727CBAF84EE41708CFBDFEFAFCC7FE67BDDBE3D0B0288C17AD5144_FieldInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral02E2E668C8A3E66E1B71CCE751383BA2E6202154;
IL2CPP_EXTERN_C String_t* _stringLiteral26DC9E6A16A2B862023CCDC29E42E5404E9ACD4F;
IL2CPP_EXTERN_C String_t* _stringLiteral53AE641F8120A58E4CA4579D3C3EF49C955143AA;
IL2CPP_EXTERN_C String_t* _stringLiteral62552877DE43E86D17785B14474A26838CEABE62;
IL2CPP_EXTERN_C String_t* _stringLiteral720C8000E99AA541958AB9377A45EAD264414570;
IL2CPP_EXTERN_C String_t* _stringLiteralC1280CC9C5E5B35BC9A007CD6F8A74B3067589BF;
IL2CPP_EXTERN_C String_t* _stringLiteralDD0CB2B041D4213B2DF270C297BE4F470467D769;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncUniTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisAwaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_m45147CA6FD245141D603BE53FFA00BEC11307AE4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncUniTaskMethodBuilder_1_Create_mA2EA6B1A2E9D6FE72E21C228FF2BDCF7568B6287_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncUniTaskMethodBuilder_1_SetException_m58656B0D6CFE20EA8DD35F7206EC2CD8B3CA19F8_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncUniTaskMethodBuilder_1_SetResult_m5D3144FF5E57B43F6E1E65292F308D4C969D6AAD_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncUniTaskMethodBuilder_1_SetStateMachine_m55900588106B84428C413B9F29949608564A83F2_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncUniTaskMethodBuilder_1_Start_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_mB9E119A34C26C840D91A21B24B8A3D7CA9BC27C1_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AsyncUniTaskMethodBuilder_1_get_Task_mFD7244F71DAD3AB7BF6DB1B06238E0CA40220AB5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Awaiter_GetResult_m6A92BBCDFE9E7A7762AA393590D6AA2B465E5A18_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Awaiter_get_IsCompleted_m0A46D603914BB8C00F8FAEAFD1799838764062B4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* UniTask_1_GetAwaiter_mFC2A8DFB66BD57771B5313A9326BF4944DC4589D_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* YooAssetKit_LoadAssetAsync_TisYooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_m1B8260F39F8300A0603F36C5D38E992B9BD67BB5_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;
struct Exception_t_marshaled_com;
struct Exception_t_marshaled_pinvoke;

struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
struct U3CModuleU3E_t4E72A6578209D9AFF47728FB443F273042EFBB4A 
{
};
struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD  : public RuntimeObject
{
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ____items;
	int32_t ____size;
	int32_t ____version;
	RuntimeObject* ____syncRoot;
};
struct U3CPrivateImplementationDetailsU3E_tCFC97FDA94A6A475F91ECDEA9CCE1D657CE6E51D  : public RuntimeObject
{
};
struct AwaiterActions_t5D05CAC006FDEBCF6B65E2B9224BC4B44783BBE5  : public RuntimeObject
{
};
struct String_t  : public RuntimeObject
{
	int32_t ____stringLength;
	Il2CppChar ____firstChar;
};
struct UnitySourceGeneratedAssemblyMonoScriptTypes_v1_t5EB3EA56A025AD174972132A644696526652F878  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};
struct AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF 
{
	RuntimeObject* ___runnerPromise;
	Exception_t* ___ex;
	RuntimeObject* ___result;
};
struct AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F 
{
	RuntimeObject* ___runnerPromise;
	Exception_t* ___ex;
	YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* ___result;
};
struct UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 
{
	RuntimeObject* ___source;
	RuntimeObject* ___result;
	int16_t ___token;
};
struct UniTask_1_t93CCF7291BB04FEF41C11887B9CE4F52DAF0A4FE 
{
	RuntimeObject* ___source;
	YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* ___result;
	int16_t ___token;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	bool ___m_value;
};
struct Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3 
{
	uint8_t ___m_value;
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};
struct Int16_tB8EF286A9C33492FA6E6D6E67320BE93E794A175 
{
	int16_t ___m_value;
};
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	int32_t ___m_value;
};
struct IntPtr_t 
{
	void* ___m_value;
};
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915 
{
	union
	{
		struct
		{
		};
		uint8_t Void_t4861ACF8F4594C3437BB48B6E56783494B843915__padding[1];
	};
};
#pragma pack(push, tp, 1)
struct __StaticArrayInitTypeSizeU3D41_tF13487168C7410D49978C3918CE4BE5B55E1A97E 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D41_tF13487168C7410D49978C3918CE4BE5B55E1A97E__padding[41];
	};
};
#pragma pack(pop, tp)
#pragma pack(push, tp, 1)
struct __StaticArrayInitTypeSizeU3D77_t99E90357AE1ECEF930A00DA0ED90A1CF47509B7E 
{
	union
	{
		struct
		{
			union
			{
			};
		};
		uint8_t __StaticArrayInitTypeSizeU3D77_t99E90357AE1ECEF930A00DA0ED90A1CF47509B7E__padding[77];
	};
};
#pragma pack(pop, tp)
struct MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9 
{
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___FilePathsData;
	ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* ___TypesData;
	int32_t ___TotalTypes;
	int32_t ___TotalFiles;
	bool ___IsEditorOnly;
};
struct MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshaled_pinvoke
{
	Il2CppSafeArray* ___FilePathsData;
	Il2CppSafeArray* ___TypesData;
	int32_t ___TotalTypes;
	int32_t ___TotalFiles;
	int32_t ___IsEditorOnly;
};
struct MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshaled_com
{
	Il2CppSafeArray* ___FilePathsData;
	Il2CppSafeArray* ___TypesData;
	int32_t ___TotalTypes;
	int32_t ___TotalFiles;
	int32_t ___IsEditorOnly;
};
struct Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101 
{
	UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 ___task;
};
struct Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6 
{
	UniTask_1_t93CCF7291BB04FEF41C11887B9CE4F52DAF0A4FE ___task;
};
struct Delegate_t  : public RuntimeObject
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	RuntimeObject* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	bool ___method_is_virtual;
};
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr;
	intptr_t ___invoke_impl;
	Il2CppIUnknown* ___m_target;
	intptr_t ___method;
	intptr_t ___delegate_trampoline;
	intptr_t ___extra_arg;
	intptr_t ___method_code;
	intptr_t ___interp_method;
	intptr_t ___interp_invoke_impl;
	MethodInfo_t* ___method_info;
	MethodInfo_t* ___original_method_info;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data;
	int32_t ___method_is_virtual;
};
struct Exception_t  : public RuntimeObject
{
	String_t* ____className;
	String_t* ____message;
	RuntimeObject* ____data;
	Exception_t* ____innerException;
	String_t* ____helpURL;
	RuntimeObject* ____stackTrace;
	String_t* ____stackTraceString;
	String_t* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	RuntimeObject* ____dynamicMethods;
	int32_t ____HResult;
	String_t* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	IntPtrU5BU5D_tFD177F8C806A6921AD7150264CCC62FA00CAD832* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_pinvoke
{
	char* ____className;
	char* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_pinvoke* ____innerException;
	char* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	char* ____stackTraceString;
	char* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	char* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Exception_t_marshaled_com
{
	Il2CppChar* ____className;
	Il2CppChar* ____message;
	RuntimeObject* ____data;
	Exception_t_marshaled_com* ____innerException;
	Il2CppChar* ____helpURL;
	Il2CppIUnknown* ____stackTrace;
	Il2CppChar* ____stackTraceString;
	Il2CppChar* ____remoteStackTraceString;
	int32_t ____remoteStackIndex;
	Il2CppIUnknown* ____dynamicMethods;
	int32_t ____HResult;
	Il2CppChar* ____source;
	SafeSerializationManager_tCBB85B95DFD1634237140CD892E82D06ECB3F5E6* ____safeSerializationManager;
	StackTraceU5BU5D_t32FBCB20930EAF5BAE3F450FF75228E5450DA0DF* ___captured_traces;
	Il2CppSafeArray* ___native_trace_ips;
	int32_t ___caught_in_unmanaged;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr;
};
struct RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 
{
	intptr_t ___value;
};
struct UniTaskStatus_tC898C29839EBB5DB7055C3DF299A2C276237CB70 
{
	int32_t ___value__;
};
struct MulticastDelegate_t  : public Delegate_t
{
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates;
};
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates;
};
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates;
};
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_pinvoke : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
};
struct ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A_marshaled_com : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
};
struct U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760 
{
	int32_t ___U3CU3E1__state;
	AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F ___U3CU3Et__builder;
	Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6 ___U3CU3Eu__1;
};
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87  : public MulticastDelegate_t
{
};
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07  : public MulticastDelegate_t
{
};
struct YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879  : public ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A
{
	List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* ___AOTMetaAssemblies;
	List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* ___HotfixAssemblies;
	String_t* ___AOTCodesPath;
	String_t* ___HotfixCodesPath;
	String_t* ___mYooAssetHybridCLRSettingSourcePath;
	String_t* ___mYooAssetHybridCLRSettingDesPath;
};
struct List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD_StaticFields
{
	StringU5BU5D_t7674CD946EC0CE7B3AE0BE70E6EE85F2ECD9F248* ___s_emptyArray;
};
struct U3CPrivateImplementationDetailsU3E_tCFC97FDA94A6A475F91ECDEA9CCE1D657CE6E51D_StaticFields
{
	__StaticArrayInitTypeSizeU3D41_tF13487168C7410D49978C3918CE4BE5B55E1A97E ___9619B8502CE689EE4D94200200B950E684692769E7FF60F546E418310BFB325E;
	__StaticArrayInitTypeSizeU3D77_t99E90357AE1ECEF930A00DA0ED90A1CF47509B7E ___984E4CB0C0727CBAF84EE41708CFBDFEFAFCC7FE67BDDBE3D0B0288C17AD5144;
};
struct AwaiterActions_t5D05CAC006FDEBCF6B65E2B9224BC4B44783BBE5_StaticFields
{
	Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* ___InvokeContinuationDelegate;
};
struct String_t_StaticFields
{
	String_t* ___Empty;
};
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	String_t* ___TrueString;
	String_t* ___FalseString;
};
struct Exception_t_StaticFields
{
	RuntimeObject* ___s_EDILock;
};
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject;
};
struct YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_StaticFields
{
	String_t* ___YooAssetHybridCLRSettingsPath;
	YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* ____instance;
};
#ifdef __clang__
#pragma clang diagnostic pop
#endif
struct ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031  : public RuntimeArray
{
	ALIGN_FIELD (8) uint8_t m_Items[1];

	inline uint8_t GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline uint8_t* GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, uint8_t value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
	}
	inline uint8_t GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline uint8_t* GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, uint8_t value)
	{
		m_Items[index] = value;
	}
};


IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF AsyncUniTaskMethodBuilder_1_Create_m52C639DF67F73453007FA68B265675DECDD5BFF3_gshared_inline (const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AsyncUniTaskMethodBuilder_1_Start_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_mF5BC1EA7355FE7D13F39956963C776121C833153_gshared_inline (AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF* __this, U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* ___0_stateMachine, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 AsyncUniTaskMethodBuilder_1_get_Task_mE4E6D276C3ACA66E64925FE38BB3AC7749E0C793_gshared_inline (AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 YooAssetKit_LoadAssetAsync_TisRuntimeObject_m2FB10FDFEB46D8FB3A9D48B89FAE218078E8CAC1_gshared (String_t* ___0_assetName, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101 UniTask_1_GetAwaiter_m8F3FFCADC2A1177E31B359D162E7784C30F8DC5E_gshared_inline (UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Awaiter_get_IsCompleted_m64CB466F8C142312263397E37C6298E842139126_gshared_inline (Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AsyncUniTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisAwaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_m22C98349C5D27044E06B9E95297E9B85A92CB3AB_gshared_inline (AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF* __this, Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101* ___0_awaiter, U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* ___1_stateMachine, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Awaiter_GetResult_mD47D2D1B93A9A867CBEDAC2D7033F31DBCE10C3B_gshared_inline (Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101* __this, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AsyncUniTaskMethodBuilder_1_SetException_m8F65BBE4BCD00BC5CC3C50B468EDCB3AABCB09A7_gshared_inline (AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF* __this, Exception_t* ___0_exception, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AsyncUniTaskMethodBuilder_1_SetResult_m9E784ED4CDCCF8F431EFFD0EF46AEB02E48CABE7_gshared_inline (AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF* __this, RuntimeObject* ___0_result, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AsyncUniTaskMethodBuilder_1_SetStateMachine_m8A43AF59E5FC7FC4CA56454D17EC7F00FDD57916_gshared (AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF* __this, RuntimeObject* ___0_stateMachine, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 UniTask_FromException_TisRuntimeObject_mDF1ABC422AE3F6DAE340E96C8990714611928198_gshared (Exception_t* ___0_ex, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 UniTask_FromResult_TisRuntimeObject_m984DFA43C55807E5425B4E96EFB4B4303DFDB7F6_gshared (RuntimeObject* ___0_value, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Awaiter__ctor_mA4F4E868662E0BD80C0BB0E125DEC5A22CEE886E_gshared_inline (Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101* __this, UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* ___0_task, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t UniTask_1_get_Status_mA5CFB18FB86A0F36F682D8E814F9A25CE84D87C5_gshared_inline (UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AsyncUniTask_2_SetStateMachine_m190B8922DADEAA9794B646BB1FDC0A3D0BECED0F_gshared (U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* ___0_stateMachine, RuntimeObject** ___1_runnerPromiseFieldRef, const RuntimeMethod* method) ;
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Awaiter_UnsafeOnCompleted_m1CCB04A74D166784C6CDEFAA8AA67879A0CCE793_gshared_inline (Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101* __this, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___0_continuation, const RuntimeMethod* method) ;

IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B (RuntimeArray* ___0_array, RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 ___1_fldHandle, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Application_get_dataPath_m4C8412CBEE4EAAAB6711CC9BEFFA73CEE5BDBEF7 (const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Path_Combine_mA07781A88C6F9067A942D5C11B9703DA8518E4C3 (String_t* ___0_path1, String_t* ___1_path2, String_t* ___2_path3, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void ScriptableObject__ctor_mD037FDB0B487295EA47F79A4DB1BF1846C9087FF (ScriptableObject_tB3BFDB921A1B1795B38A5417D3B97A89A140436A* __this, const RuntimeMethod* method) ;
inline void List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
inline AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F AsyncUniTaskMethodBuilder_1_Create_mA2EA6B1A2E9D6FE72E21C228FF2BDCF7568B6287_inline (const RuntimeMethod* method)
{
	return ((  AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F (*) (const RuntimeMethod*))AsyncUniTaskMethodBuilder_1_Create_m52C639DF67F73453007FA68B265675DECDD5BFF3_gshared_inline)(method);
}
inline void AsyncUniTaskMethodBuilder_1_Start_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_mB9E119A34C26C840D91A21B24B8A3D7CA9BC27C1_inline (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F* __this, U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* ___0_stateMachine, const RuntimeMethod* method)
{
	((  void (*) (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F*, U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760*, const RuntimeMethod*))AsyncUniTaskMethodBuilder_1_Start_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_mF5BC1EA7355FE7D13F39956963C776121C833153_gshared_inline)(__this, ___0_stateMachine, method);
}
inline UniTask_1_t93CCF7291BB04FEF41C11887B9CE4F52DAF0A4FE AsyncUniTaskMethodBuilder_1_get_Task_mFD7244F71DAD3AB7BF6DB1B06238E0CA40220AB5_inline (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F* __this, const RuntimeMethod* method)
{
	return ((  UniTask_1_t93CCF7291BB04FEF41C11887B9CE4F52DAF0A4FE (*) (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F*, const RuntimeMethod*))AsyncUniTaskMethodBuilder_1_get_Task_mE4E6D276C3ACA66E64925FE38BB3AC7749E0C793_gshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
inline UniTask_1_t93CCF7291BB04FEF41C11887B9CE4F52DAF0A4FE YooAssetKit_LoadAssetAsync_TisYooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_m1B8260F39F8300A0603F36C5D38E992B9BD67BB5 (String_t* ___0_assetName, const RuntimeMethod* method)
{
	return ((  UniTask_1_t93CCF7291BB04FEF41C11887B9CE4F52DAF0A4FE (*) (String_t*, const RuntimeMethod*))YooAssetKit_LoadAssetAsync_TisRuntimeObject_m2FB10FDFEB46D8FB3A9D48B89FAE218078E8CAC1_gshared)(___0_assetName, method);
}
inline Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6 UniTask_1_GetAwaiter_mFC2A8DFB66BD57771B5313A9326BF4944DC4589D_inline (UniTask_1_t93CCF7291BB04FEF41C11887B9CE4F52DAF0A4FE* __this, const RuntimeMethod* method)
{
	return ((  Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6 (*) (UniTask_1_t93CCF7291BB04FEF41C11887B9CE4F52DAF0A4FE*, const RuntimeMethod*))UniTask_1_GetAwaiter_m8F3FFCADC2A1177E31B359D162E7784C30F8DC5E_gshared_inline)(__this, method);
}
inline bool Awaiter_get_IsCompleted_m0A46D603914BB8C00F8FAEAFD1799838764062B4_inline (Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6*, const RuntimeMethod*))Awaiter_get_IsCompleted_m64CB466F8C142312263397E37C6298E842139126_gshared_inline)(__this, method);
}
inline void AsyncUniTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisAwaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_m45147CA6FD245141D603BE53FFA00BEC11307AE4_inline (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F* __this, Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6* ___0_awaiter, U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* ___1_stateMachine, const RuntimeMethod* method)
{
	((  void (*) (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F*, Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6*, U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760*, const RuntimeMethod*))AsyncUniTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisAwaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_m22C98349C5D27044E06B9E95297E9B85A92CB3AB_gshared_inline)(__this, ___0_awaiter, ___1_stateMachine, method);
}
inline YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* Awaiter_GetResult_m6A92BBCDFE9E7A7762AA393590D6AA2B465E5A18_inline (Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6* __this, const RuntimeMethod* method)
{
	return ((  YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* (*) (Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6*, const RuntimeMethod*))Awaiter_GetResult_mD47D2D1B93A9A867CBEDAC2D7033F31DBCE10C3B_gshared_inline)(__this, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m9E3155FB84015C823606188F53B47CB44C444991 (String_t* ___0_str0, String_t* ___1_str1, const RuntimeMethod* method) ;
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
inline void AsyncUniTaskMethodBuilder_1_SetException_m58656B0D6CFE20EA8DD35F7206EC2CD8B3CA19F8_inline (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F* __this, Exception_t* ___0_exception, const RuntimeMethod* method)
{
	((  void (*) (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F*, Exception_t*, const RuntimeMethod*))AsyncUniTaskMethodBuilder_1_SetException_m8F65BBE4BCD00BC5CC3C50B468EDCB3AABCB09A7_gshared_inline)(__this, ___0_exception, method);
}
inline void AsyncUniTaskMethodBuilder_1_SetResult_m5D3144FF5E57B43F6E1E65292F308D4C969D6AAD_inline (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F* __this, YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* ___0_result, const RuntimeMethod* method)
{
	((  void (*) (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F*, YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879*, const RuntimeMethod*))AsyncUniTaskMethodBuilder_1_SetResult_m9E784ED4CDCCF8F431EFFD0EF46AEB02E48CABE7_gshared_inline)(__this, ___0_result, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CGetInstanceAsyncU3Ed__15_MoveNext_m69A2D8FC584FC16A2869590156493DCC1AB791DB (U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* __this, const RuntimeMethod* method) ;
inline void AsyncUniTaskMethodBuilder_1_SetStateMachine_m55900588106B84428C413B9F29949608564A83F2 (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F* __this, RuntimeObject* ___0_stateMachine, const RuntimeMethod* method)
{
	((  void (*) (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F*, RuntimeObject*, const RuntimeMethod*))AsyncUniTaskMethodBuilder_1_SetStateMachine_m8A43AF59E5FC7FC4CA56454D17EC7F00FDD57916_gshared)(__this, ___0_stateMachine, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CGetInstanceAsyncU3Ed__15_SetStateMachine_mE0B0429EF6DFEBB8464F06D5586F4B7A8174F908 (U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* __this, RuntimeObject* ___0_stateMachine, const RuntimeMethod* method) ;
inline UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 UniTask_FromException_TisRuntimeObject_mDF1ABC422AE3F6DAE340E96C8990714611928198 (Exception_t* ___0_ex, const RuntimeMethod* method)
{
	return ((  UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 (*) (Exception_t*, const RuntimeMethod*))UniTask_FromException_TisRuntimeObject_mDF1ABC422AE3F6DAE340E96C8990714611928198_gshared)(___0_ex, method);
}
inline UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 UniTask_FromResult_TisRuntimeObject_m984DFA43C55807E5425B4E96EFB4B4303DFDB7F6 (RuntimeObject* ___0_value, const RuntimeMethod* method)
{
	return ((  UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 (*) (RuntimeObject*, const RuntimeMethod*))UniTask_FromResult_TisRuntimeObject_m984DFA43C55807E5425B4E96EFB4B4303DFDB7F6_gshared)(___0_value, method);
}
inline void Awaiter__ctor_mA4F4E868662E0BD80C0BB0E125DEC5A22CEE886E_inline (Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101* __this, UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* ___0_task, const RuntimeMethod* method)
{
	((  void (*) (Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101*, UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440*, const RuntimeMethod*))Awaiter__ctor_mA4F4E868662E0BD80C0BB0E125DEC5A22CEE886E_gshared_inline)(__this, ___0_task, method);
}
inline int32_t UniTask_1_get_Status_mA5CFB18FB86A0F36F682D8E814F9A25CE84D87C5_inline (UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440*, const RuntimeMethod*))UniTask_1_get_Status_mA5CFB18FB86A0F36F682D8E814F9A25CE84D87C5_gshared_inline)(__this, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool UniTaskStatusExtensions_IsCompleted_mF43C41C9CEB640E381D1F7A8B40142843AED87AE_inline (int32_t ___0_status, const RuntimeMethod* method) ;
inline void AsyncUniTask_2_SetStateMachine_m190B8922DADEAA9794B646BB1FDC0A3D0BECED0F (U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* ___0_stateMachine, RuntimeObject** ___1_runnerPromiseFieldRef, const RuntimeMethod* method)
{
	((  void (*) (U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760*, RuntimeObject**, const RuntimeMethod*))AsyncUniTask_2_SetStateMachine_m190B8922DADEAA9794B646BB1FDC0A3D0BECED0F_gshared)(___0_stateMachine, ___1_runnerPromiseFieldRef, method);
}
inline void Awaiter_UnsafeOnCompleted_m1CCB04A74D166784C6CDEFAA8AA67879A0CCE793_inline (Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101* __this, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___0_continuation, const RuntimeMethod* method)
{
	((  void (*) (Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101*, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*, const RuntimeMethod*))Awaiter_UnsafeOnCompleted_m1CCB04A74D166784C6CDEFAA8AA67879A0CCE793_gshared_inline)(__this, ___0_continuation, method);
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) ;
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9 UnitySourceGeneratedAssemblyMonoScriptTypes_v1_Get_m75A21C7912AAE0ADDA1DEA6A1D83A62F38B24441 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CPrivateImplementationDetailsU3E_tCFC97FDA94A6A475F91ECDEA9CCE1D657CE6E51D____9619B8502CE689EE4D94200200B950E684692769E7FF60F546E418310BFB325E_FieldInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CPrivateImplementationDetailsU3E_tCFC97FDA94A6A475F91ECDEA9CCE1D657CE6E51D____984E4CB0C0727CBAF84EE41708CFBDFEFAFCC7FE67BDDBE3D0B0288C17AD5144_FieldInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		il2cpp_codegen_initobj((&V_0), sizeof(MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9));
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_0 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)SZArrayNew(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var, (uint32_t)((int32_t)77));
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_1 = L_0;
		RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 L_2 = { reinterpret_cast<intptr_t> (U3CPrivateImplementationDetailsU3E_tCFC97FDA94A6A475F91ECDEA9CCE1D657CE6E51D____984E4CB0C0727CBAF84EE41708CFBDFEFAFCC7FE67BDDBE3D0B0288C17AD5144_FieldInfo_var) };
		RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B((RuntimeArray*)L_1, L_2, NULL);
		(&V_0)->___FilePathsData = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&(&V_0)->___FilePathsData), (void*)L_1);
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_3 = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)SZArrayNew(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031_il2cpp_TypeInfo_var, (uint32_t)((int32_t)41));
		ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031* L_4 = L_3;
		RuntimeFieldHandle_t6E4C45B6D2EA12FC99185805A7E77527899B25C5 L_5 = { reinterpret_cast<intptr_t> (U3CPrivateImplementationDetailsU3E_tCFC97FDA94A6A475F91ECDEA9CCE1D657CE6E51D____9619B8502CE689EE4D94200200B950E684692769E7FF60F546E418310BFB325E_FieldInfo_var) };
		RuntimeHelpers_InitializeArray_m751372AA3F24FBF6DA9B9D687CBFA2DE436CAB9B((RuntimeArray*)L_4, L_5, NULL);
		(&V_0)->___TypesData = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&(&V_0)->___TypesData), (void*)L_4);
		(&V_0)->___TotalFiles = 1;
		(&V_0)->___TotalTypes = 1;
		(&V_0)->___IsEditorOnly = (bool)0;
		MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9 L_6 = V_0;
		return L_6;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void UnitySourceGeneratedAssemblyMonoScriptTypes_v1__ctor_m37EC2708362CC43E1261619918A77C3D33A98D75 (UnitySourceGeneratedAssemblyMonoScriptTypes_v1_t5EB3EA56A025AD174972132A644696526652F878* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C void MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshal_pinvoke(const MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9& unmarshaled, MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshaled_pinvoke& marshaled)
{
	marshaled.___FilePathsData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___FilePathsData);
	marshaled.___TypesData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___TypesData);
	marshaled.___TotalTypes = unmarshaled.___TotalTypes;
	marshaled.___TotalFiles = unmarshaled.___TotalFiles;
	marshaled.___IsEditorOnly = static_cast<int32_t>(unmarshaled.___IsEditorOnly);
}
IL2CPP_EXTERN_C void MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshal_pinvoke_back(const MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshaled_pinvoke& marshaled, MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9& unmarshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	unmarshaled.___FilePathsData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, marshaled.___FilePathsData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___FilePathsData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, marshaled.___FilePathsData));
	unmarshaled.___TypesData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, marshaled.___TypesData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___TypesData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, marshaled.___TypesData));
	int32_t unmarshaledTotalTypes_temp_2 = 0;
	unmarshaledTotalTypes_temp_2 = marshaled.___TotalTypes;
	unmarshaled.___TotalTypes = unmarshaledTotalTypes_temp_2;
	int32_t unmarshaledTotalFiles_temp_3 = 0;
	unmarshaledTotalFiles_temp_3 = marshaled.___TotalFiles;
	unmarshaled.___TotalFiles = unmarshaledTotalFiles_temp_3;
	bool unmarshaledIsEditorOnly_temp_4 = false;
	unmarshaledIsEditorOnly_temp_4 = static_cast<bool>(marshaled.___IsEditorOnly);
	unmarshaled.___IsEditorOnly = unmarshaledIsEditorOnly_temp_4;
}
IL2CPP_EXTERN_C void MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshal_pinvoke_cleanup(MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshaled_pinvoke& marshaled)
{
	il2cpp_codegen_com_destroy_safe_array(marshaled.___FilePathsData);
	marshaled.___FilePathsData = NULL;
	il2cpp_codegen_com_destroy_safe_array(marshaled.___TypesData);
	marshaled.___TypesData = NULL;
}
IL2CPP_EXTERN_C void MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshal_com(const MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9& unmarshaled, MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshaled_com& marshaled)
{
	marshaled.___FilePathsData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___FilePathsData);
	marshaled.___TypesData = il2cpp_codegen_com_marshal_safe_array(IL2CPP_VT_I1, unmarshaled.___TypesData);
	marshaled.___TotalTypes = unmarshaled.___TotalTypes;
	marshaled.___TotalFiles = unmarshaled.___TotalFiles;
	marshaled.___IsEditorOnly = static_cast<int32_t>(unmarshaled.___IsEditorOnly);
}
IL2CPP_EXTERN_C void MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshal_com_back(const MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshaled_com& marshaled, MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9& unmarshaled)
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	unmarshaled.___FilePathsData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, marshaled.___FilePathsData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___FilePathsData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, marshaled.___FilePathsData));
	unmarshaled.___TypesData = (ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, marshaled.___TypesData);
	Il2CppCodeGenWriteBarrier((void**)(&unmarshaled.___TypesData), (void*)(ByteU5BU5D_tA6237BF417AE52AD70CFB4EF24A7A82613DF9031*)il2cpp_codegen_com_marshal_safe_array_result(IL2CPP_VT_I1, Byte_t94D9231AC217BE4D2E004C4CD32DF6D099EA41A3_il2cpp_TypeInfo_var, marshaled.___TypesData));
	int32_t unmarshaledTotalTypes_temp_2 = 0;
	unmarshaledTotalTypes_temp_2 = marshaled.___TotalTypes;
	unmarshaled.___TotalTypes = unmarshaledTotalTypes_temp_2;
	int32_t unmarshaledTotalFiles_temp_3 = 0;
	unmarshaledTotalFiles_temp_3 = marshaled.___TotalFiles;
	unmarshaled.___TotalFiles = unmarshaledTotalFiles_temp_3;
	bool unmarshaledIsEditorOnly_temp_4 = false;
	unmarshaledIsEditorOnly_temp_4 = static_cast<bool>(marshaled.___IsEditorOnly);
	unmarshaled.___IsEditorOnly = unmarshaledIsEditorOnly_temp_4;
}
IL2CPP_EXTERN_C void MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshal_com_cleanup(MonoScriptData_tB26CF0C22E3EE11966690481936EC8D313B0D2D9_marshaled_com& marshaled)
{
	il2cpp_codegen_com_destroy_safe_array(marshaled.___FilePathsData);
	marshaled.___FilePathsData = NULL;
	il2cpp_codegen_com_destroy_safe_array(marshaled.___TypesData);
	marshaled.___TypesData = NULL;
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* YooAssetHybridCLRSetting_get_YooAssetHybridCLRSettingSourcePath_m67FCCD4BEB7E30F1446588685621F554DC02288B (YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Application_tDB03BE91CDF0ACA614A5E0B67CFB77C44EB19B21_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral26DC9E6A16A2B862023CCDC29E42E5404E9ACD4F);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Application_tDB03BE91CDF0ACA614A5E0B67CFB77C44EB19B21_il2cpp_TypeInfo_var);
		String_t* L_0;
		L_0 = Application_get_dataPath_m4C8412CBEE4EAAAB6711CC9BEFFA73CEE5BDBEF7(NULL);
		String_t* L_1 = __this->___mYooAssetHybridCLRSettingSourcePath;
		il2cpp_codegen_runtime_class_init_inline(Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		String_t* L_2;
		L_2 = Path_Combine_mA07781A88C6F9067A942D5C11B9703DA8518E4C3(L_0, _stringLiteral26DC9E6A16A2B862023CCDC29E42E5404E9ACD4F, L_1, NULL);
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* YooAssetHybridCLRSetting_get_YooAssetHybridCLRSettingDesPath_mB08895A26BCED82D66244BF645CAC008D39E72E0 (YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Application_tDB03BE91CDF0ACA614A5E0B67CFB77C44EB19B21_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral26DC9E6A16A2B862023CCDC29E42E5404E9ACD4F);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(Application_tDB03BE91CDF0ACA614A5E0B67CFB77C44EB19B21_il2cpp_TypeInfo_var);
		String_t* L_0;
		L_0 = Application_get_dataPath_m4C8412CBEE4EAAAB6711CC9BEFFA73CEE5BDBEF7(NULL);
		String_t* L_1 = __this->___mYooAssetHybridCLRSettingDesPath;
		il2cpp_codegen_runtime_class_init_inline(Path_t8A38A801D0219E8209C1B1D90D82D4D755D998BC_il2cpp_TypeInfo_var);
		String_t* L_2;
		L_2 = Path_Combine_mA07781A88C6F9067A942D5C11B9703DA8518E4C3(L_0, _stringLiteral26DC9E6A16A2B862023CCDC29E42E5404E9ACD4F, L_1, NULL);
		return L_2;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void YooAssetHybridCLRSetting__ctor_m77270364564206A0BC1EA7EFB33F9D6F743427CC (YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral02E2E668C8A3E66E1B71CCE751383BA2E6202154);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral62552877DE43E86D17785B14474A26838CEABE62);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral720C8000E99AA541958AB9377A45EAD264414570);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralC1280CC9C5E5B35BC9A007CD6F8A74B3067589BF);
		s_Il2CppMethodInitialized = true;
	}
	{
		__this->___AOTCodesPath = _stringLiteral720C8000E99AA541958AB9377A45EAD264414570;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___AOTCodesPath), (void*)_stringLiteral720C8000E99AA541958AB9377A45EAD264414570);
		__this->___HotfixCodesPath = _stringLiteral62552877DE43E86D17785B14474A26838CEABE62;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___HotfixCodesPath), (void*)_stringLiteral62552877DE43E86D17785B14474A26838CEABE62);
		__this->___mYooAssetHybridCLRSettingSourcePath = _stringLiteralC1280CC9C5E5B35BC9A007CD6F8A74B3067589BF;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mYooAssetHybridCLRSettingSourcePath), (void*)_stringLiteralC1280CC9C5E5B35BC9A007CD6F8A74B3067589BF);
		__this->___mYooAssetHybridCLRSettingDesPath = _stringLiteral02E2E668C8A3E66E1B71CCE751383BA2E6202154;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mYooAssetHybridCLRSettingDesPath), (void*)_stringLiteral02E2E668C8A3E66E1B71CCE751383BA2E6202154);
		ScriptableObject__ctor_mD037FDB0B487295EA47F79A4DB1BF1846C9087FF(__this, NULL);
		List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* L_0 = (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD*)il2cpp_codegen_object_new(List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD_il2cpp_TypeInfo_var);
		List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E(L_0, List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E_RuntimeMethod_var);
		__this->___AOTMetaAssemblies = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___AOTMetaAssemblies), (void*)L_0);
		List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD* L_1 = (List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD*)il2cpp_codegen_object_new(List_1_tF470A3BE5C1B5B68E1325EF3F109D172E60BD7CD_il2cpp_TypeInfo_var);
		List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E(L_1, List_1__ctor_mCA8DD57EAC70C2B5923DBB9D5A77CEAC22E7068E_RuntimeMethod_var);
		__this->___HotfixAssemblies = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___HotfixAssemblies), (void*)L_1);
		return;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* YooAssetHybridCLRSetting_get_Instance_m883DCB973C3F682864BE963357D431A43B60EA30 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var);
		YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* L_0 = ((YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_StaticFields*)il2cpp_codegen_static_fields_for(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var))->____instance;
		return L_0;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR UniTask_1_t93CCF7291BB04FEF41C11887B9CE4F52DAF0A4FE YooAssetHybridCLRSetting_GetInstanceAsync_mE7FE3506279C50501E383A5276B81E184EEE95C7 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncUniTaskMethodBuilder_1_Create_mA2EA6B1A2E9D6FE72E21C228FF2BDCF7568B6287_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncUniTaskMethodBuilder_1_Start_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_mB9E119A34C26C840D91A21B24B8A3D7CA9BC27C1_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncUniTaskMethodBuilder_1_get_Task_mFD7244F71DAD3AB7BF6DB1B06238E0CA40220AB5_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F L_0;
		L_0 = AsyncUniTaskMethodBuilder_1_Create_mA2EA6B1A2E9D6FE72E21C228FF2BDCF7568B6287_inline(AsyncUniTaskMethodBuilder_1_Create_mA2EA6B1A2E9D6FE72E21C228FF2BDCF7568B6287_RuntimeMethod_var);
		(&V_0)->___U3CU3Et__builder = L_0;
		Il2CppCodeGenWriteBarrier((void**)&(((&(&V_0)->___U3CU3Et__builder))->___runnerPromise), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&(((&(&V_0)->___U3CU3Et__builder))->___ex), (void*)NULL);
		#endif
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&(((&(&V_0)->___U3CU3Et__builder))->___result), (void*)NULL);
		#endif
		(&V_0)->___U3CU3E1__state = (-1);
		AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F* L_1 = (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F*)(&(&V_0)->___U3CU3Et__builder);
		AsyncUniTaskMethodBuilder_1_Start_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_mB9E119A34C26C840D91A21B24B8A3D7CA9BC27C1_inline(L_1, (&V_0), AsyncUniTaskMethodBuilder_1_Start_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_mB9E119A34C26C840D91A21B24B8A3D7CA9BC27C1_RuntimeMethod_var);
		AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F* L_2 = (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F*)(&(&V_0)->___U3CU3Et__builder);
		UniTask_1_t93CCF7291BB04FEF41C11887B9CE4F52DAF0A4FE L_3;
		L_3 = AsyncUniTaskMethodBuilder_1_get_Task_mFD7244F71DAD3AB7BF6DB1B06238E0CA40220AB5_inline(L_2, AsyncUniTaskMethodBuilder_1_get_Task_mFD7244F71DAD3AB7BF6DB1B06238E0CA40220AB5_RuntimeMethod_var);
		return L_3;
	}
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void YooAssetHybridCLRSetting__cctor_m845647580494DA35F555D02380FAA80E6C99A9BF (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralDD0CB2B041D4213B2DF270C297BE4F470467D769);
		s_Il2CppMethodInitialized = true;
	}
	{
		((YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_StaticFields*)il2cpp_codegen_static_fields_for(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var))->___YooAssetHybridCLRSettingsPath = _stringLiteralDD0CB2B041D4213B2DF270C297BE4F470467D769;
		Il2CppCodeGenWriteBarrier((void**)(&((YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_StaticFields*)il2cpp_codegen_static_fields_for(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var))->___YooAssetHybridCLRSettingsPath), (void*)_stringLiteralDD0CB2B041D4213B2DF270C297BE4F470467D769);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CGetInstanceAsyncU3Ed__15_MoveNext_m69A2D8FC584FC16A2869590156493DCC1AB791DB (U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncUniTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisAwaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_m45147CA6FD245141D603BE53FFA00BEC11307AE4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncUniTaskMethodBuilder_1_SetResult_m5D3144FF5E57B43F6E1E65292F308D4C969D6AAD_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Awaiter_GetResult_m6A92BBCDFE9E7A7762AA393590D6AA2B465E5A18_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Awaiter_get_IsCompleted_m0A46D603914BB8C00F8FAEAFD1799838764062B4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UniTask_1_GetAwaiter_mFC2A8DFB66BD57771B5313A9326BF4944DC4589D_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&YooAssetKit_LoadAssetAsync_TisYooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_m1B8260F39F8300A0603F36C5D38E992B9BD67BB5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral53AE641F8120A58E4CA4579D3C3EF49C955143AA);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* V_1 = NULL;
	Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6 V_2;
	memset((&V_2), 0, sizeof(V_2));
	UniTask_1_t93CCF7291BB04FEF41C11887B9CE4F52DAF0A4FE V_3;
	memset((&V_3), 0, sizeof(V_3));
	Exception_t* V_4 = NULL;
	il2cpp::utils::ExceptionSupportStack<RuntimeObject*, 1> __active_exceptions;
	{
		int32_t L_0 = __this->___U3CU3E1__state;
		V_0 = L_0;
	}
	try
	{
		{
			int32_t L_1 = V_0;
			if (!L_1)
			{
				goto IL_0056_1;
			}
		}
		{
			il2cpp_codegen_runtime_class_init_inline(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var);
			YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* L_2 = ((YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_StaticFields*)il2cpp_codegen_static_fields_for(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var))->____instance;
			il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
			bool L_3;
			L_3 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_2, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
			if (!L_3)
			{
				goto IL_009f_1;
			}
		}
		{
			il2cpp_codegen_runtime_class_init_inline(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var);
			String_t* L_4 = ((YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_StaticFields*)il2cpp_codegen_static_fields_for(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var))->___YooAssetHybridCLRSettingsPath;
			UniTask_1_t93CCF7291BB04FEF41C11887B9CE4F52DAF0A4FE L_5;
			L_5 = YooAssetKit_LoadAssetAsync_TisYooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_m1B8260F39F8300A0603F36C5D38E992B9BD67BB5(L_4, YooAssetKit_LoadAssetAsync_TisYooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_m1B8260F39F8300A0603F36C5D38E992B9BD67BB5_RuntimeMethod_var);
			V_3 = L_5;
			Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6 L_6;
			L_6 = UniTask_1_GetAwaiter_mFC2A8DFB66BD57771B5313A9326BF4944DC4589D_inline((&V_3), UniTask_1_GetAwaiter_mFC2A8DFB66BD57771B5313A9326BF4944DC4589D_RuntimeMethod_var);
			V_2 = L_6;
			bool L_7;
			L_7 = Awaiter_get_IsCompleted_m0A46D603914BB8C00F8FAEAFD1799838764062B4_inline((&V_2), Awaiter_get_IsCompleted_m0A46D603914BB8C00F8FAEAFD1799838764062B4_RuntimeMethod_var);
			if (L_7)
			{
				goto IL_0072_1;
			}
		}
		{
			int32_t L_8 = 0;
			V_0 = L_8;
			__this->___U3CU3E1__state = L_8;
			Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6 L_9 = V_2;
			__this->___U3CU3Eu__1 = L_9;
			Il2CppCodeGenWriteBarrier((void**)&((&(((&__this->___U3CU3Eu__1))->___task))->___source), (void*)NULL);
			#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
			Il2CppCodeGenWriteBarrier((void**)&((&(((&__this->___U3CU3Eu__1))->___task))->___result), (void*)NULL);
			#endif
			AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F* L_10 = (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F*)(&__this->___U3CU3Et__builder);
			AsyncUniTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisAwaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_m45147CA6FD245141D603BE53FFA00BEC11307AE4_inline(L_10, (&V_2), __this, AsyncUniTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisAwaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_m45147CA6FD245141D603BE53FFA00BEC11307AE4_RuntimeMethod_var);
			goto IL_00d4;
		}

IL_0056_1:
		{
			Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6 L_11 = __this->___U3CU3Eu__1;
			V_2 = L_11;
			Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6* L_12 = (Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6*)(&__this->___U3CU3Eu__1);
			il2cpp_codegen_initobj(L_12, sizeof(Awaiter_t6C2CF7F1E283B361DFAE4DFDF2B7955FD6C6D3B6));
			int32_t L_13 = (-1);
			V_0 = L_13;
			__this->___U3CU3E1__state = L_13;
		}

IL_0072_1:
		{
			YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* L_14;
			L_14 = Awaiter_GetResult_m6A92BBCDFE9E7A7762AA393590D6AA2B465E5A18_inline((&V_2), Awaiter_GetResult_m6A92BBCDFE9E7A7762AA393590D6AA2B465E5A18_RuntimeMethod_var);
			il2cpp_codegen_runtime_class_init_inline(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var);
			((YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_StaticFields*)il2cpp_codegen_static_fields_for(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var))->____instance = L_14;
			Il2CppCodeGenWriteBarrier((void**)(&((YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_StaticFields*)il2cpp_codegen_static_fields_for(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var))->____instance), (void*)L_14);
			YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* L_15 = ((YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_StaticFields*)il2cpp_codegen_static_fields_for(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var))->____instance;
			il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
			bool L_16;
			L_16 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_15, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
			if (!L_16)
			{
				goto IL_009f_1;
			}
		}
		{
			il2cpp_codegen_runtime_class_init_inline(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var);
			String_t* L_17 = ((YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_StaticFields*)il2cpp_codegen_static_fields_for(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var))->___YooAssetHybridCLRSettingsPath;
			String_t* L_18;
			L_18 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral53AE641F8120A58E4CA4579D3C3EF49C955143AA, L_17, NULL);
			il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
			Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(L_18, NULL);
		}

IL_009f_1:
		{
			il2cpp_codegen_runtime_class_init_inline(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var);
			YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* L_19 = ((YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_StaticFields*)il2cpp_codegen_static_fields_for(YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879_il2cpp_TypeInfo_var))->____instance;
			V_1 = L_19;
			goto IL_00c0;
		}
	}
	catch(Il2CppExceptionWrapper& e)
	{
		if(il2cpp_codegen_class_is_assignable_from (((RuntimeClass*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&Exception_t_il2cpp_TypeInfo_var)), il2cpp_codegen_object_class(e.ex)))
		{
			IL2CPP_PUSH_ACTIVE_EXCEPTION(e.ex);
			goto CATCH_00a7;
		}
		throw e;
	}

CATCH_00a7:
	{
		Exception_t* L_20 = ((Exception_t*)IL2CPP_GET_ACTIVE_EXCEPTION(Exception_t*));;
		V_4 = L_20;
		__this->___U3CU3E1__state = ((int32_t)-2);
		AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F* L_21 = (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F*)(&__this->___U3CU3Et__builder);
		Exception_t* L_22 = V_4;
		AsyncUniTaskMethodBuilder_1_SetException_m58656B0D6CFE20EA8DD35F7206EC2CD8B3CA19F8_inline(L_21, L_22, ((RuntimeMethod*)il2cpp_codegen_initialize_runtime_metadata_inline((uintptr_t*)&AsyncUniTaskMethodBuilder_1_SetException_m58656B0D6CFE20EA8DD35F7206EC2CD8B3CA19F8_RuntimeMethod_var)));
		IL2CPP_POP_ACTIVE_EXCEPTION(Exception_t*);
		goto IL_00d4;
	}

IL_00c0:
	{
		__this->___U3CU3E1__state = ((int32_t)-2);
		AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F* L_23 = (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F*)(&__this->___U3CU3Et__builder);
		YooAssetHybridCLRSetting_t2824C739403711F7C6A2A16AA4E198468CEC5879* L_24 = V_1;
		AsyncUniTaskMethodBuilder_1_SetResult_m5D3144FF5E57B43F6E1E65292F308D4C969D6AAD_inline(L_23, L_24, AsyncUniTaskMethodBuilder_1_SetResult_m5D3144FF5E57B43F6E1E65292F308D4C969D6AAD_RuntimeMethod_var);
	}

IL_00d4:
	{
		return;
	}
}
IL2CPP_EXTERN_C  void U3CGetInstanceAsyncU3Ed__15_MoveNext_m69A2D8FC584FC16A2869590156493DCC1AB791DB_AdjustorThunk (RuntimeObject* __this, const RuntimeMethod* method)
{
	U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760*>(__this + _offset);
	U3CGetInstanceAsyncU3Ed__15_MoveNext_m69A2D8FC584FC16A2869590156493DCC1AB791DB(_thisAdjusted, method);
}
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CGetInstanceAsyncU3Ed__15_SetStateMachine_mE0B0429EF6DFEBB8464F06D5586F4B7A8174F908 (U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* __this, RuntimeObject* ___0_stateMachine, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AsyncUniTaskMethodBuilder_1_SetStateMachine_m55900588106B84428C413B9F29949608564A83F2_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F* L_0 = (AsyncUniTaskMethodBuilder_1_tDD1938EDE569764BE2727E6A2AB5D020F7EBE12F*)(&__this->___U3CU3Et__builder);
		RuntimeObject* L_1 = ___0_stateMachine;
		AsyncUniTaskMethodBuilder_1_SetStateMachine_m55900588106B84428C413B9F29949608564A83F2(L_0, L_1, AsyncUniTaskMethodBuilder_1_SetStateMachine_m55900588106B84428C413B9F29949608564A83F2_RuntimeMethod_var);
		return;
	}
}
IL2CPP_EXTERN_C  void U3CGetInstanceAsyncU3Ed__15_SetStateMachine_mE0B0429EF6DFEBB8464F06D5586F4B7A8174F908_AdjustorThunk (RuntimeObject* __this, RuntimeObject* ___0_stateMachine, const RuntimeMethod* method)
{
	U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* _thisAdjusted;
	int32_t _offset = 1;
	_thisAdjusted = reinterpret_cast<U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760*>(__this + _offset);
	U3CGetInstanceAsyncU3Ed__15_SetStateMachine_mE0B0429EF6DFEBB8464F06D5586F4B7A8174F908(_thisAdjusted, ___0_stateMachine, method);
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF AsyncUniTaskMethodBuilder_1_Create_m52C639DF67F73453007FA68B265675DECDD5BFF3_gshared_inline (const RuntimeMethod* method) 
{
	AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		il2cpp_codegen_initobj((&V_0), sizeof(AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF));
		AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF L_0 = V_0;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AsyncUniTaskMethodBuilder_1_Start_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_mF5BC1EA7355FE7D13F39956963C776121C833153_gshared_inline (AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF* __this, U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* ___0_stateMachine, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* L_0 = ___0_stateMachine;
		U3CGetInstanceAsyncU3Ed__15_MoveNext_m69A2D8FC584FC16A2869590156493DCC1AB791DB(L_0, il2cpp_rgctx_method(method->rgctx_data, 2));
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 AsyncUniTaskMethodBuilder_1_get_Task_mE4E6D276C3ACA66E64925FE38BB3AC7749E0C793_gshared_inline (AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&UniTask_t8E1453C1D8424B1FC22B0E51B017D3B028E17270_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		RuntimeObject* L_0 = __this->___runnerPromise;
		if (!L_0)
		{
			goto IL_0014;
		}
	}
	{
		RuntimeObject* L_1 = __this->___runnerPromise;
		NullCheck(L_1);
		UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 L_2;
		L_2 = InterfaceFuncInvoker0< UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 >::Invoke(1, il2cpp_rgctx_data(InitializedTypeInfo(method->klass)->rgctx_data, 1), L_1);
		return L_2;
	}

IL_0014:
	{
		Exception_t* L_3 = __this->___ex;
		if (!L_3)
		{
			goto IL_0028;
		}
	}
	{
		Exception_t* L_4 = __this->___ex;
		il2cpp_codegen_runtime_class_init_inline(UniTask_t8E1453C1D8424B1FC22B0E51B017D3B028E17270_il2cpp_TypeInfo_var);
		UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 L_5;
		L_5 = UniTask_FromException_TisRuntimeObject_mDF1ABC422AE3F6DAE340E96C8990714611928198(L_4, il2cpp_rgctx_method(InitializedTypeInfo(method->klass)->rgctx_data, 4));
		return L_5;
	}

IL_0028:
	{
		RuntimeObject* L_6 = __this->___result;
		il2cpp_codegen_runtime_class_init_inline(UniTask_t8E1453C1D8424B1FC22B0E51B017D3B028E17270_il2cpp_TypeInfo_var);
		UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 L_7;
		L_7 = UniTask_FromResult_TisRuntimeObject_m984DFA43C55807E5425B4E96EFB4B4303DFDB7F6(L_6, il2cpp_rgctx_method(InitializedTypeInfo(method->klass)->rgctx_data, 6));
		return L_7;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101 UniTask_1_GetAwaiter_m8F3FFCADC2A1177E31B359D162E7784C30F8DC5E_gshared_inline (UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* __this, const RuntimeMethod* method) 
{
	{
		Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101 L_0;
		memset((&L_0), 0, sizeof(L_0));
		Awaiter__ctor_mA4F4E868662E0BD80C0BB0E125DEC5A22CEE886E_inline((&L_0), __this, il2cpp_rgctx_method(InitializedTypeInfo(method->klass)->rgctx_data, 5));
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool Awaiter_get_IsCompleted_m64CB466F8C142312263397E37C6298E842139126_gshared_inline (Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101* __this, const RuntimeMethod* method) 
{
	{
		UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* L_0 = (UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440*)(&__this->___task);
		int32_t L_1;
		L_1 = UniTask_1_get_Status_mA5CFB18FB86A0F36F682D8E814F9A25CE84D87C5_inline(L_0, il2cpp_rgctx_method(InitializedTypeInfo(method->klass)->rgctx_data, 3));
		bool L_2;
		L_2 = UniTaskStatusExtensions_IsCompleted_mF43C41C9CEB640E381D1F7A8B40142843AED87AE_inline(L_1, NULL);
		return L_2;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AsyncUniTaskMethodBuilder_1_AwaitUnsafeOnCompleted_TisAwaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101_TisU3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760_m22C98349C5D27044E06B9E95297E9B85A92CB3AB_gshared_inline (AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF* __this, Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101* ___0_awaiter, U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* ___1_stateMachine, const RuntimeMethod* method) 
{
	il2cpp_rgctx_method_init(method);
	{
		RuntimeObject* L_0 = __this->___runnerPromise;
		if (L_0)
		{
			goto IL_0014;
		}
	}
	{
		U3CGetInstanceAsyncU3Ed__15_t3F491F2C1CA227D5F3368AEB03CCC3012F285760* L_1 = ___1_stateMachine;
		RuntimeObject** L_2 = (RuntimeObject**)(&__this->___runnerPromise);
		il2cpp_codegen_runtime_class_init_inline(il2cpp_rgctx_data(method->rgctx_data, 2));
		AsyncUniTask_2_SetStateMachine_m190B8922DADEAA9794B646BB1FDC0A3D0BECED0F(L_1, L_2, il2cpp_rgctx_method(method->rgctx_data, 1));
	}

IL_0014:
	{
		Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101* L_3 = ___0_awaiter;
		RuntimeObject* L_4 = __this->___runnerPromise;
		NullCheck(L_4);
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_5;
		L_5 = InterfaceFuncInvoker0< Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* >::Invoke(0, il2cpp_rgctx_data(InitializedTypeInfo(method->klass)->rgctx_data, 1), L_4);
		Awaiter_UnsafeOnCompleted_m1CCB04A74D166784C6CDEFAA8AA67879A0CCE793_inline(L_3, L_5, il2cpp_rgctx_method(method->rgctx_data, 5));
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* Awaiter_GetResult_mD47D2D1B93A9A867CBEDAC2D7033F31DBCE10C3B_gshared_inline (Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101* __this, const RuntimeMethod* method) 
{
	RuntimeObject* V_0 = NULL;
	{
		UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* L_0 = (UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440*)(&__this->___task);
		RuntimeObject* L_1 = L_0->___source;
		V_0 = L_1;
		RuntimeObject* L_2 = V_0;
		if (L_2)
		{
			goto IL_001b;
		}
	}
	{
		UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* L_3 = (UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440*)(&__this->___task);
		RuntimeObject* L_4 = L_3->___result;
		return L_4;
	}

IL_001b:
	{
		RuntimeObject* L_5 = V_0;
		UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* L_6 = (UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440*)(&__this->___task);
		int16_t L_7 = L_6->___token;
		NullCheck(L_5);
		RuntimeObject* L_8;
		L_8 = InterfaceFuncInvoker1< RuntimeObject*, int16_t >::Invoke(0, il2cpp_rgctx_data(InitializedTypeInfo(method->klass)->rgctx_data, 5), L_5, L_7);
		return L_8;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AsyncUniTaskMethodBuilder_1_SetException_m8F65BBE4BCD00BC5CC3C50B468EDCB3AABCB09A7_gshared_inline (AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF* __this, Exception_t* ___0_exception, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___runnerPromise;
		if (L_0)
		{
			goto IL_0010;
		}
	}
	{
		Exception_t* L_1 = ___0_exception;
		__this->___ex = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___ex), (void*)L_1);
		return;
	}

IL_0010:
	{
		RuntimeObject* L_2 = __this->___runnerPromise;
		Exception_t* L_3 = ___0_exception;
		NullCheck(L_2);
		InterfaceActionInvoker1< Exception_t* >::Invoke(3, il2cpp_rgctx_data(InitializedTypeInfo(method->klass)->rgctx_data, 1), L_2, L_3);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AsyncUniTaskMethodBuilder_1_SetResult_m9E784ED4CDCCF8F431EFFD0EF46AEB02E48CABE7_gshared_inline (AsyncUniTaskMethodBuilder_1_tFC09635241966209A887C377AE6C642AC59B4DEF* __this, RuntimeObject* ___0_result, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___runnerPromise;
		if (L_0)
		{
			goto IL_0010;
		}
	}
	{
		RuntimeObject* L_1 = ___0_result;
		__this->___result = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___result), (void*)L_1);
		return;
	}

IL_0010:
	{
		RuntimeObject* L_2 = __this->___runnerPromise;
		RuntimeObject* L_3 = ___0_result;
		NullCheck(L_2);
		InterfaceActionInvoker1< RuntimeObject* >::Invoke(2, il2cpp_rgctx_data(InitializedTypeInfo(method->klass)->rgctx_data, 1), L_2, L_3);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool UniTaskStatusExtensions_IsCompleted_mF43C41C9CEB640E381D1F7A8B40142843AED87AE_inline (int32_t ___0_status, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = ___0_status;
		return (bool)((!(((uint32_t)L_0) <= ((uint32_t)0)))? 1 : 0);
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Awaiter__ctor_mA4F4E868662E0BD80C0BB0E125DEC5A22CEE886E_gshared_inline (Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101* __this, UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* ___0_task, const RuntimeMethod* method) 
{
	{
		UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* L_0 = ___0_task;
		UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440 L_1 = (*(UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440*)L_0);
		__this->___task = L_1;
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___task))->___source), (void*)NULL);
		#if IL2CPP_ENABLE_STRICT_WRITE_BARRIERS
		Il2CppCodeGenWriteBarrier((void**)&(((&__this->___task))->___result), (void*)NULL);
		#endif
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t UniTask_1_get_Status_mA5CFB18FB86A0F36F682D8E814F9A25CE84D87C5_gshared_inline (UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___source;
		if (!L_0)
		{
			goto IL_001a;
		}
	}
	{
		RuntimeObject* L_1 = __this->___source;
		int16_t L_2 = __this->___token;
		NullCheck(L_1);
		int32_t L_3;
		L_3 = InterfaceFuncInvoker1< int32_t, int16_t >::Invoke(1, il2cpp_rgctx_data(InitializedTypeInfo(method->klass)->rgctx_data, 1), L_1, L_2);
		return L_3;
	}

IL_001a:
	{
		return (int32_t)(1);
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Awaiter_UnsafeOnCompleted_m1CCB04A74D166784C6CDEFAA8AA67879A0CCE793_gshared_inline (Awaiter_t0767B80BBE72D416CD4F41DA0FB54CD61AFEB101* __this, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___0_continuation, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AwaiterActions_t5D05CAC006FDEBCF6B65E2B9224BC4B44783BBE5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	{
		UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* L_0 = (UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440*)(&__this->___task);
		RuntimeObject* L_1 = L_0->___source;
		V_0 = L_1;
		RuntimeObject* L_2 = V_0;
		if (L_2)
		{
			goto IL_0016;
		}
	}
	{
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_3 = ___0_continuation;
		NullCheck(L_3);
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(L_3, NULL);
		return;
	}

IL_0016:
	{
		RuntimeObject* L_4 = V_0;
		il2cpp_codegen_runtime_class_init_inline(AwaiterActions_t5D05CAC006FDEBCF6B65E2B9224BC4B44783BBE5_il2cpp_TypeInfo_var);
		Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* L_5 = ((AwaiterActions_t5D05CAC006FDEBCF6B65E2B9224BC4B44783BBE5_StaticFields*)il2cpp_codegen_static_fields_for(AwaiterActions_t5D05CAC006FDEBCF6B65E2B9224BC4B44783BBE5_il2cpp_TypeInfo_var))->___InvokeContinuationDelegate;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_6 = ___0_continuation;
		UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440* L_7 = (UniTask_1_tB1B04E29E602E879F73EEA52804796621BBFE440*)(&__this->___task);
		int16_t L_8 = L_7->___token;
		NullCheck(L_4);
		InterfaceActionInvoker3< Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87*, RuntimeObject*, int16_t >::Invoke(2, il2cpp_rgctx_data(InitializedTypeInfo(method->klass)->rgctx_data, 5), L_4, L_5, (RuntimeObject*)L_6, L_8);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl)((Il2CppObject*)__this->___method_code, reinterpret_cast<RuntimeMethod*>(__this->___method));
}
