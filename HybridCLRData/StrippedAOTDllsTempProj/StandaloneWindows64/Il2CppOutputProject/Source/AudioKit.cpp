#include "pch-cpp.hpp"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include <limits>


template <typename R>
struct VirtualFuncInvoker0
{
	typedef R (*Func)(void*, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, invokeData.method);
	}
};
template <typename R, typename T1>
struct VirtualFuncInvoker1
{
	typedef R (*Func)(void*, T1, const RuntimeMethod*);

	static inline R Invoke (Il2CppMethodSlot slot, RuntimeObject* obj, T1 p1)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_virtual_invoke_data(slot, obj);
		return ((Func)invokeData.methodPtr)(obj, p1, invokeData.method);
	}
};
struct InterfaceActionInvoker0
{
	typedef void (*Action)(void*, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, invokeData.method);
	}
};
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
template <typename T1, typename T2>
struct InterfaceActionInvoker2
{
	typedef void (*Action)(void*, T1, T2, const RuntimeMethod*);

	static inline void Invoke (Il2CppMethodSlot slot, RuntimeClass* declaringInterface, RuntimeObject* obj, T1 p1, T2 p2)
	{
		const VirtualInvokeData& invokeData = il2cpp_codegen_get_interface_invoke_data(slot, obj, declaringInterface);
		((Action)invokeData.methodPtr)(obj, p1, p2, invokeData.method);
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

// QFramework.AbstractAction`1<System.Object>
struct AbstractAction_1_t1F25168840FE7E9F00A1ACAB29227E65D6713A34;
// QFramework.AbstractAction`1<QFramework.PlaySoundAction>
struct AbstractAction_1_t9A1EECE1BD7DD95509513E65E672298D45093CD3;
// System.Action`1<UnityEngine.AsyncOperation>
struct Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB;
// System.Action`1<QFramework.AudioPlayer>
struct Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219;
// System.Action`1<System.Boolean>
struct Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C;
// System.Action`1<System.Int32>
struct Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404;
// System.Action`1<System.Object>
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87;
// System.Action`1<System.Single>
struct Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A;
// System.Action`2<System.Boolean,UnityEngine.AudioClip>
struct Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD;
// System.Action`2<System.Boolean,System.Object>
struct Action_2_t4E94B0FCA1084D7868DB11A50767A4916CA3D3FB;
// QFramework.BinaryHeap`1<System.Object>
struct BinaryHeap_1_tB7A2124313D61037F773E68E0596B02D8F126DF7;
// QFramework.BinaryHeap`1<QFramework.TimeItem>
struct BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03;
// QFramework.BindableProperty`1<System.Boolean>
struct BindableProperty_1_t1ACEAE0BD8CBD20B51A6CACA4F4B56BE6F680D8A;
// QFramework.BindableProperty`1<System.Single>
struct BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58;
// QFramework.CustomProperty`1<System.Boolean>
struct CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A;
// System.Collections.Generic.Dictionary`2<System.Object,System.Int32>
struct Dictionary_2_t5C96F4B6841710A9013966F76224BAE01FB4B4D1;
// System.Collections.Generic.Dictionary`2<System.Object,System.Object>
struct Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA;
// System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>
struct Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032;
// System.Collections.Generic.Dictionary`2<System.String,System.Int32>
struct Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588;
// QFramework.EasyEvent`1<System.Boolean>
struct EasyEvent_1_t77E890713BCD88F7DC5E46CA7FDFBBF8638CE97F;
// QFramework.EasyEvent`1<System.Single>
struct EasyEvent_1_tE0C0E3729F504114DE09195290DF83C2096BFBF9;
// System.Func`1<System.Boolean>
struct Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457;
// System.Func`2<System.Collections.Generic.KeyValuePair`2<System.Object,System.Object>,System.Collections.Generic.IEnumerable`1<System.Object>>
struct Func_2_t10FDFF9EE1C80F76FB5C31BA1D127F21CF49F9DA;
// System.Func`2<System.Collections.Generic.KeyValuePair`2<System.Object,System.Object>,System.Object>
struct Func_2_tF42287527472FA89789873F068A87C60A00EC7D3;
// System.Func`2<System.Collections.Generic.KeyValuePair`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>,System.Collections.Generic.IEnumerable`1<QFramework.AudioPlayer>>
struct Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB;
// System.Func`3<System.Boolean,System.Boolean,System.Boolean>
struct Func_3_t8405246FA4174D01D5F07F8A51737739EE5376F3;
// System.Func`3<System.Single,System.Single,System.Boolean>
struct Func_3_tA9AA477D8A5A68C7DC26AE4792295B80F920E61E;
// System.Collections.Generic.IEnumerable`1<System.Collections.Generic.KeyValuePair`2<System.Object,System.Object>>
struct IEnumerable_1_t60509816D8966320E2A9660FC756B6C440ADFC50;
// System.Collections.Generic.IEnumerable`1<System.Collections.Generic.KeyValuePair`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>>
struct IEnumerable_1_tC5412EB0902532068846BE22BEE37F2FA5CC393A;
// System.Collections.Generic.IEnumerable`1<QFramework.AudioPlayer>
struct IEnumerable_1_t84CEB7B86C0AC2FEB137388487931364B31CF457;
// System.Collections.Generic.IEnumerable`1<System.Object>
struct IEnumerable_1_tF95C9E01A913DD50575531C8305932628663D9E9;
// System.Collections.Generic.IEqualityComparer`1<System.String>
struct IEqualityComparer_1_tAE94C8F24AD5B94D4EE85CA9FC59E3409D41CAF7;
// QFramework.IObjectFactory`1<QFramework.AudioPlayer>
struct IObjectFactory_1_t0B3FC60E27AD640B850AB22A1786363FBED78A76;
// QFramework.IObjectFactory`1<QFramework.AudioSearchKeys>
struct IObjectFactory_1_t66BED46555B2D3FA214AAF74CB284EC243A46149;
// QFramework.IObjectFactory`1<QFramework.FluentMusicAPI>
struct IObjectFactory_1_t02731912DFB5F55FAD542BFCD434A5B62E9D7E07;
// QFramework.IObjectFactory`1<QFramework.FluentSoundAPI>
struct IObjectFactory_1_t9D1966BA51DA1D85A25082B1FC9CB18CBD6CDBD0;
// QFramework.IObjectFactory`1<QFramework.TimeItem>
struct IObjectFactory_1_t5E02385FB9D027CE4DE73B7C8A977BE79A925B18;
// System.Collections.Generic.Dictionary`2/KeyCollection<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>
struct KeyCollection_tBC8A0ABABA2BA5CB88A7AE4EBA75FC82CA41FF56;
// System.Collections.Generic.Dictionary`2/KeyCollection<System.String,System.Int32>
struct KeyCollection_tCC15D033281A6593E2488FAF5B205812A152AC03;
// System.Collections.Generic.List`1<QFramework.AudioPlayer>
struct List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D;
// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D;
// System.Collections.Generic.List`1<QFramework.TimeExtend.Timer>
struct List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2;
// System.Predicate`1<System.Object>
struct Predicate_1_t8342C85FF4E41CD1F7024AC0CDC3E5312A32CB12;
// System.Predicate`1<QFramework.TimeExtend.Timer>
struct Predicate_1_t55DAAC67F486E666A2115F381B43C27ADF0B01CF;
// QFramework.Property`1<System.Int32>
struct Property_1_tEA2D0DDF24CE19765ADE39EFBB400F89850D7DA8;
// QFramework.SafeObjectPool`1<QFramework.AudioPlayer>
struct SafeObjectPool_1_t3542EC4AA4871EFF26264DA85DCDB6C39D22A8AD;
// QFramework.SafeObjectPool`1<QFramework.AudioSearchKeys>
struct SafeObjectPool_1_t7D55BDF522ED56ECFF3CBDB6115923D35053E535;
// QFramework.SafeObjectPool`1<QFramework.FluentMusicAPI>
struct SafeObjectPool_1_t33C1137434D07D7A43A1A8C7D46F1C04B665E6F0;
// QFramework.SafeObjectPool`1<QFramework.FluentSoundAPI>
struct SafeObjectPool_1_t7FA207C564A34D5D5E4DE789675614FD0D0964A2;
// QFramework.SafeObjectPool`1<System.Object>
struct SafeObjectPool_1_t73BF818AB38FF94B7DEB8D5A485D353870E066B5;
// QFramework.SafeObjectPool`1<QFramework.TimeItem>
struct SafeObjectPool_1_tB9A3A3095B574142CF0139B7323BA66C80DCC342;
// QFramework.SimpleObjectPool`1<QFramework.PlaySoundAction>
struct SimpleObjectPool_1_t708F067E82816266FD6BDBC4567EE7E93D476A88;
// System.Collections.Generic.Stack`1<QFramework.AudioPlayer>
struct Stack_1_t683E816BBF299A013486DA1C8015CC306FB4DECD;
// System.Collections.Generic.Stack`1<QFramework.AudioSearchKeys>
struct Stack_1_tEFFDDAE2E7445394E87DA99660C3DEB7C8FB6C20;
// System.Collections.Generic.Stack`1<QFramework.FluentMusicAPI>
struct Stack_1_t7754B9A71A1966949423FB645745741161E6823A;
// System.Collections.Generic.Stack`1<QFramework.FluentSoundAPI>
struct Stack_1_t52C58EFF37E67FD5CD39872F5F118DF404F30BD7;
// System.Collections.Generic.Stack`1<QFramework.IAudioLoader>
struct Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5;
// System.Collections.Generic.Stack`1<System.Object>
struct Stack_1_tAD790A47551563636908E21E4F08C54C0C323EB5;
// System.Collections.Generic.Stack`1<QFramework.TimeItem>
struct Stack_1_t5AB6E9C738A9F2D44B2728233EDA003BA43495F1;
// UnityEngine.Events.UnityEvent`1<System.Boolean>
struct UnityEvent_1_tEEB36A367DCB5867E93AAF6BECAF3558CA71BECB;
// UnityEngine.Events.UnityEvent`1<System.Int32>
struct UnityEvent_1_t7CC0661D6B113117B4CC68761D93AC8DF5DBD66A;
// System.Collections.Generic.Dictionary`2/ValueCollection<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>
struct ValueCollection_t4D61321B108E87AD0AFADD941958B3236130A38A;
// System.Collections.Generic.Dictionary`2/ValueCollection<System.String,System.Int32>
struct ValueCollection_tCE6BD704B9571C131E2D8C8CED569DDEC4AE042B;
// System.Collections.Generic.Dictionary`2/Entry<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>[]
struct EntryU5BU5D_t10D4C073E3F32C63686B741D8EC8F59927ADF680;
// System.Collections.Generic.Dictionary`2/Entry<System.String,System.Int32>[]
struct EntryU5BU5D_tEA0133B78B9FF7045128C508FA50247E525A94D6;
// QFramework.AudioPlayer[]
struct AudioPlayerU5BU5D_t7846E47045539DC7F01D62590FB9608E0F1328D4;
// System.Char[]
struct CharU5BU5D_t799905CF001DD5F13F7DBB310181FC4D8B7D0AAB;
// System.Delegate[]
struct DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771;
// QFramework.IAudioLoader[]
struct IAudioLoaderU5BU5D_t7651B2DEA9F10E6C148192FCFFB72DFD1528ED64;
// System.Int32[]
struct Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C;
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;
// QFramework.TimeItem[]
struct TimeItemU5BU5D_t8144BE62A6EE673DF12BBA5A1F8826696EBD58D1;
// QFramework.TimeExtend.Timer[]
struct TimerU5BU5D_t0388C5B52BCEDF995E3BE21CDA5048FB6317F5F5;
// QFramework.AbstractAudioLoaderPool
struct AbstractAudioLoaderPool_tA9514ECF9B0920914A41DA1E94F9C7914109136A;
// System.Action
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07;
// UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C;
// UnityEngine.AudioClip
struct AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20;
// QFramework.AudioKit
struct AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2;
// QFramework.AudioKitConfig
struct AudioKitConfig_t0518724CE4F14A672627665BE74D533751686A54;
// QFramework.AudioKitSettings
struct AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8;
// UnityEngine.AudioListener
struct AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35;
// QFramework.AudioManager
struct AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274;
// QFramework.AudioPlayer
struct AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59;
// QFramework.AudioSearchKeys
struct AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3;
// UnityEngine.AudioSource
struct AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299;
// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3;
// QFramework.DefaultAudioLoader
struct DefaultAudioLoader_tA58E6CBC81292CF195A2FED8AF13ABF1473CF43F;
// QFramework.DefaultAudioLoaderPool
struct DefaultAudioLoaderPool_t83CA69E29CA127237BADCBB67697143A0ED7D53A;
// System.Delegate
struct Delegate_t;
// System.DelegateData
struct DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E;
// QFramework.FluentMusicAPI
struct FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A;
// QFramework.FluentSoundAPI
struct FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C;
// UnityEngine.GameObject
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F;
// QFramework.IAction
struct IAction_t5FFDF691AF2AD64371C3E61F75A9DA448ECC9992;
// QFramework.IAudioKitOnFinish
struct IAudioKitOnFinish_tF0FC1CC3932C215418825A77BF074D98BA83E79C;
// QFramework.IAudioLoader
struct IAudioLoader_t02005AF5330B4CD269CC97AB97F7A9510BE4D79A;
// QFramework.IAudioLoaderPool
struct IAudioLoaderPool_t25A31F4FC2301F1B46FC33A0BF0C2E1F65589A22;
// QFramework.ISequence
struct ISequence_tFCEF100F3A0B8679A84CF63ED6D265A2607536D4;
// QFramework.IUnRegister
struct IUnRegister_t7B430C330D5B51832A032E0B0BEB969376633E1F;
// QFramework.IntProperty
struct IntProperty_t4E5B676427BABBB583A5A88D57DA2C0BCF45A10A;
// System.Reflection.MethodInfo
struct MethodInfo_t;
// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71;
// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C;
// QFramework.PlaySoundAction
struct PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909;
// QFramework.PlayerPrefsBooleanProperty
struct PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3;
// QFramework.PlayerPrefsFloatProperty
struct PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793;
// UnityEngine.ResourceRequest
struct ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868;
// System.String
struct String_t;
// QFramework.TimeItem
struct TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8;
// QFramework.Timer
struct Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D;
// QFramework.TimeExtend.Timer
struct Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4;
// QFramework.TimeExtend.TimerDriver
struct TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2;
// UnityEngine.Transform
struct Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1;
// System.Type
struct Type_t;
// System.Void
struct Void_t4861ACF8F4594C3437BB48B6E56783494B843915;
// UnityEngine.AudioClip/PCMReaderCallback
struct PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E;
// UnityEngine.AudioClip/PCMSetPositionCallback
struct PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072;
// QFramework.AudioKit/<>c
struct U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC;
// QFramework.AudioKit/<>c__DisplayClass21_0
struct U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157;
// QFramework.AudioKit/<>c__DisplayClass25_0
struct U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51;
// QFramework.AudioManager/<>c
struct U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86;
// QFramework.DefaultAudioLoader/<>c__DisplayClass4_0
struct U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B;
// QFramework.TimeExtend.Timer/<>c__DisplayClass15_0
struct U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA;

IL2CPP_EXTERN_C RuntimeClass* AbstractAction_1_t9A1EECE1BD7DD95509513E65E672298D45093CD3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AudioKitConfig_t0518724CE4F14A672627665BE74D533751686A54_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DefaultAudioLoaderPool_t83CA69E29CA127237BADCBB67697143A0ED7D53A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* DefaultAudioLoader_tA58E6CBC81292CF195A2FED8AF13ABF1473CF43F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* GameObject_t76FEDD663AB33C991A9C9A23129337651094216F_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IAudioKitOnFinish_tF0FC1CC3932C215418825A77BF074D98BA83E79C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IAudioLoaderPool_t25A31F4FC2301F1B46FC33A0BF0C2E1F65589A22_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IAudioLoader_t02005AF5330B4CD269CC97AB97F7A9510BE4D79A_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerable_1_t84CEB7B86C0AC2FEB137388487931364B31CF457_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_1_t374204E15A93E57A3C60B540043B3B795485AC96_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ISequence_tFCEF100F3A0B8679A84CF63ED6D265A2607536D4_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Predicate_1_t55DAAC67F486E666A2115F381B43C27ADF0B01CF_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C RuntimeClass* Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var;
IL2CPP_EXTERN_C String_t* _stringLiteral13B62B289497AADBE4E98E91944A655E327A7ABD;
IL2CPP_EXTERN_C String_t* _stringLiteral240BE9B5E609F0DD8657150691504C4DE92A0925;
IL2CPP_EXTERN_C String_t* _stringLiteral4916DA5F3D35D35493CBB5556B638FE8093E5A6D;
IL2CPP_EXTERN_C String_t* _stringLiteral52DAB56457890BF2D137A9A4E1BA2346E92DB450;
IL2CPP_EXTERN_C String_t* _stringLiteral728B387DF77B756CAE5319FD715930757F9B615F;
IL2CPP_EXTERN_C String_t* _stringLiteral7A9D14B94D28FB9E5C1133505C5CFB7767D6C55F;
IL2CPP_EXTERN_C String_t* _stringLiteral9BA7B8AFF7E32B71E5A1503CF07A3758837776ED;
IL2CPP_EXTERN_C String_t* _stringLiteralA3D636F286CFC52E32577AAF6E182EB8B0FDAAA3;
IL2CPP_EXTERN_C String_t* _stringLiteralAB9EB91F16EF830015769D3B7CA3749AE4A7938F;
IL2CPP_EXTERN_C String_t* _stringLiteralAD7DC71B936C20931E35C4FE34CF4D1D3056E0A5;
IL2CPP_EXTERN_C String_t* _stringLiteralBAD4A6E573B068D6167F13578714BA76E87F09CB;
IL2CPP_EXTERN_C String_t* _stringLiteralC10CC291369A31E4DE72DE6E97E5E2EF7339A12B;
IL2CPP_EXTERN_C String_t* _stringLiteralC88326BF66F1656CEB1B68D4540896C71001AE2B;
IL2CPP_EXTERN_C String_t* _stringLiteralCF47A8518514248D2A2EB59BB80CB4D2602DA048;
IL2CPP_EXTERN_C String_t* _stringLiteralE858D088F94FA9F7A1AC8D933236C82A8A396BEA;
IL2CPP_EXTERN_C String_t* _stringLiteralF0B34B7D0C316DF89CF323EDF95AF14BEF34823A;
IL2CPP_EXTERN_C const RuntimeMethod* AbstractAction_1_Allocate_m87F117921135FDFEECA4721CC49E3FA41F235F6D_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AbstractAction_1__ctor_m59DED3CAB019783282FD5DAC55A1646BF2E0A8BC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioKitSettings_U3C_ctorU3Eb__6_0_m0EA65EB385532EB11451EC4BF97849B38CE0FFE4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioKitSettings_U3C_ctorU3Eb__6_1_m193FF57433E10C590810CCC2C728EBCE870AFA50_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioManager_U3COnSingletonInitU3Eb__8_0_m9192C129BBFAF7808E1443E232C0BB55C41ACF8E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioManager_U3COnSingletonInitU3Eb__8_1_m363F918A1199AC07BB4E1053E6FEF1CE9BBA46B0_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioManager_U3COnSingletonInitU3Eb__8_2_m4D6072A151B1CFAA3F52F76E5037F101A6BB399A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioPlayer_OnResLoadFinish_m62EB8A101B47B4E722D75F817C37ED012B1A33B4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioPlayer_OnResumeTimeTick_m82DE3AC83904EE1C80B30D28C5B00BC6CBEA04B2_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioPlayer_OnSoundPlayFinish_mEED80DC657DE5D9004B528A3E174D045EE01BDED_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* AudioPlayer_SetVolume_m0306F9423FAFF5F273E941A297F11EC77D516260_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* BinaryHeap_1_Clear_mA0971C02BA613AC8B838BE7C9A470FD9D1F13EDB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* BinaryHeap_1_Insert_m0CE4B4F1F7097434EF0F96D95DD16C1592A72C8C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* BinaryHeap_1_Pop_mB0A91FAA9BBB3881F4743F07A49AC36C7B6796E1_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* BinaryHeap_1_Top_m031CA8B0E52BBAD5A48F2732884CF6CC27C325E5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* BinaryHeap_1__ctor_m3D6509830FB73EDAE5EAC9D72DF322E21B196B82_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* BindableProperty_1_RegisterWithInitValue_m50618DDD0FF1AADCBFF8D781BC9D6DE5AEA35824_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* BindableProperty_1_Register_mA9B66CC57EE9D6586607598395E59C8DA968FE87_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* BindableProperty_1_UnRegister_mDE5848741098D28A95EE63902771C3214DB883CA_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* BindableProperty_1_set_Value_m4F9D20068AFC366BBC972B46D6A54C5FC8BDE651_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* CustomProperty_1__ctor_mF71DC5C64029750FFE0EB414A8D4AA9765719458_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_Add_m2FE98C9C3763E31D7CB55207ED3A46B33BF64883_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_Add_mAC3ECB943BFF17C600E7EC191AB4A4E4C6E885F4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_Clear_m841DBE29811833266CC127714688998A50D5F7CD_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_Clear_mAAB064453A17DFB0A15C203ECF907FFA7AF7D46E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_ContainsKey_m6FE0713BCEFD16DC00E8636D5B8DD3BA313536E4_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_ContainsKey_mAEDD6BBEE1B37BC5E1D803803352FBE4CF4D3D7E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_Remove_m0C90DB78F4134CDEB001F338AA2745F8D9651CAC_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_TryGetValue_m835BB1E6EA8A8BF1242B51E28FD65B43FEF68E2A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2__ctor_mA3C3860EDE2CDD08BBD68C389377BC89D029D968_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2__ctor_mFE946D85F78DE391E71EA905A87F0BE5F34A041F_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_get_Item_mF0187134582D2A345C1E9A834E336B5A5F96DEC9_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Dictionary_2_set_Item_m038480C0EC13713DBD89A53BE69FF0359501B4C2_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Enumerable_SelectMany_TisKeyValuePair_2_tA1ACD899BEC543AE9704AB31AC4C62160B215F43_TisAudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59_mAC2CE1EF65E76E2298ACF9EC5852CA617133628D_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* FluentSoundAPI_U3CPlayU3Eb__15_0_m3D13802E1118D4232F92DA68DFB998D43FD35A95_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* FluentSoundAPI_U3CPlayU3Eb__15_1_m92225A92EF2E9F135347E99E564002BADAF775BD_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* GameObject_AddComponent_TisAudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35_mA30AC51FE6287A9D0057077D99F694600A3D844E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* GameObject_AddComponent_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_m0E8EFDB9B3D8DF1ADE10C56D3168A9C1BA19BF14_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* GameObject_AddComponent_TisTimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_m6E1162BC6BB854F682DC3C069CCE84DCC39A8411_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* KeyValuePair_2_get_Value_m267D88C38008E34C628B6E9037CB9FB98C40C334_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Add_m6320686C1369BBBA0D20DBC782155A5B2A00BAC5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Contains_m35660B24F32E8D50C67E6AE681D8E2084B5D8276_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Exists_mA5D007E602F98F4B2E9D7C5954BBAB42E8CB7888_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Remove_m32474DB046D95F7ABA9D1985B34A186E4F41CDE0_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_Remove_mE6E4B96921EE86B995707B091ABCBEEC1E894C22_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_m137F537B4FA09945E22CF1C0390E1A538D3D8F2C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1__ctor_mEB86DAF271C6934999B430DFFDA0C2B4BE67E398_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Count_mA7DA63FD079A8939EE109E799F9A48EB03198C96_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* List_1_get_Item_m593E51A37B3107E675E10D79C053FB4EE0857080_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MonoSingletonProperty_1_get_Instance_m2BF4163558BC28C3B9119589FDD486ADC669CA7F_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* MonoSingletonProperty_1_get_Instance_m9EE6F4D4D2E389D777F50092E629D09BE4E90C76_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Object_FindObjectOfType_TisAudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35_mFDA0D604F7239642B39B6010674A936ADD544912_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Object_FindObjectOfType_TisTimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_mA4C61CEE004298FBD022CDE9621561524631EE2E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* PlaySoundAction_U3COnStartU3Eb__7_0_m92466F5143CFA6D9FE73B11EE6AF81613C7A1EDE_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* PlaySoundAction_U3COnStartU3Eb__7_1_mBCE202982D6EC8BCF75D7A4C8490EC6E49450EC0_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Property_1__ctor_m77CDCAA18D9FC2F380162C19E268B4B1956628E8_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Property_1_get_Value_mAB5065F7B5D8CB3B340114A5BB2953765536E0C7_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Property_1_set_Value_m314C5C5A72F5F0400ABFB00DFC1466A3434D26F3_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Resources_LoadAsync_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m0D27E9CB67015D736F22E3AFAB5E6108ABA9A740_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Resources_Load_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m8D55846FD24C1D133D4EA744BE1E73E054B93A78_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SafeObjectPool_1_Init_mB381C8FD59EE0D59D0AB6A93C6CE4DBEA1C1CA43_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SafeObjectPool_1_get_Instance_m10308F90A4BD9A0D5A63EC07A735B36D00C075ED_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SafeObjectPool_1_get_Instance_m27DE656011C461C719BEA3A05D455044BCA2757A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SafeObjectPool_1_get_Instance_m3795D49BEBB3B1FBDA78876A916F253627F999D5_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SafeObjectPool_1_get_Instance_m462FA7E647B9136571534928DB536BDFE6AB8B86_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Stack_1_Pop_m7109081E7676FB662FEB90062F1A6C4A288E596A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Stack_1_Push_m837A07DFF4ED3EC7B750D3EF3E9DFF8E4C713DD0_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Stack_1__ctor_mDA3004D09A08CCD4F349E97A1A95B76B9D5D41F6_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* Stack_1_get_Count_mE28FA86A5FE81403FA112BE1E3D928DAFCD15F0E_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec_U3CForEachAllSoundU3Eb__10_0_m7DFE197E012B617AADBCCC71F64625325C1A9248_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec_U3COnSingletonInitU3Eb__8_3_mABE712C9E196449F677BA9735EBDE446C92C1360_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec_U3CStopAllSoundU3Eb__22_0_m90BB3B9C70D28CFE509799F933282F40E10125BB_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass15_0_U3C_ctorU3Eb__0_m9BFF9FD45188C78680ED3AE443D19FA49875CB5A_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass21_0_U3CPlaySoundU3Eb__0_mDA28D0DC610632C2F0C62AE0ECA419368839AE9C_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass25_0_U3CPlaySoundU3Eb__0_m8D85FB5C48586757CCB3D6BD15B129DC3F0038D6_RuntimeMethod_var;
IL2CPP_EXTERN_C const RuntimeMethod* U3CU3Ec__DisplayClass4_0_U3CLoadClipAsyncU3Eb__0_mF979D841FD744ADE23D11771DC3F2DA1A2799E24_RuntimeMethod_var;
struct Delegate_t_marshaled_com;
struct Delegate_t_marshaled_pinvoke;

struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918;

IL2CPP_EXTERN_C_BEGIN
IL2CPP_EXTERN_C_END

#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// <Module>
struct U3CModuleU3E_tDA831D5F487C178E058F850B6543DD948AC60486 
{
};

// QFramework.BindableProperty`1<System.Boolean>
struct BindableProperty_1_t1ACEAE0BD8CBD20B51A6CACA4F4B56BE6F680D8A  : public RuntimeObject
{
	// T QFramework.BindableProperty`1::mValue
	bool ___mValue_0;
	// QFramework.EasyEvent`1<T> QFramework.BindableProperty`1::mOnValueChanged
	EasyEvent_1_t77E890713BCD88F7DC5E46CA7FDFBBF8638CE97F* ___mOnValueChanged_2;
};

// QFramework.BindableProperty`1<System.Single>
struct BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58  : public RuntimeObject
{
	// T QFramework.BindableProperty`1::mValue
	float ___mValue_0;
	// QFramework.EasyEvent`1<T> QFramework.BindableProperty`1::mOnValueChanged
	EasyEvent_1_tE0C0E3729F504114DE09195290DF83C2096BFBF9* ___mOnValueChanged_2;
};

// System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>
struct Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032  : public RuntimeObject
{
	// System.Int32[] System.Collections.Generic.Dictionary`2::_buckets
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____buckets_0;
	// System.Collections.Generic.Dictionary`2/Entry<TKey,TValue>[] System.Collections.Generic.Dictionary`2::_entries
	EntryU5BU5D_t10D4C073E3F32C63686B741D8EC8F59927ADF680* ____entries_1;
	// System.Int32 System.Collections.Generic.Dictionary`2::_count
	int32_t ____count_2;
	// System.Int32 System.Collections.Generic.Dictionary`2::_freeList
	int32_t ____freeList_3;
	// System.Int32 System.Collections.Generic.Dictionary`2::_freeCount
	int32_t ____freeCount_4;
	// System.Int32 System.Collections.Generic.Dictionary`2::_version
	int32_t ____version_5;
	// System.Collections.Generic.IEqualityComparer`1<TKey> System.Collections.Generic.Dictionary`2::_comparer
	RuntimeObject* ____comparer_6;
	// System.Collections.Generic.Dictionary`2/KeyCollection<TKey,TValue> System.Collections.Generic.Dictionary`2::_keys
	KeyCollection_tBC8A0ABABA2BA5CB88A7AE4EBA75FC82CA41FF56* ____keys_7;
	// System.Collections.Generic.Dictionary`2/ValueCollection<TKey,TValue> System.Collections.Generic.Dictionary`2::_values
	ValueCollection_t4D61321B108E87AD0AFADD941958B3236130A38A* ____values_8;
	// System.Object System.Collections.Generic.Dictionary`2::_syncRoot
	RuntimeObject* ____syncRoot_9;
};

// System.Collections.Generic.Dictionary`2<System.String,System.Int32>
struct Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588  : public RuntimeObject
{
	// System.Int32[] System.Collections.Generic.Dictionary`2::_buckets
	Int32U5BU5D_t19C97395396A72ECAF310612F0760F165060314C* ____buckets_0;
	// System.Collections.Generic.Dictionary`2/Entry<TKey,TValue>[] System.Collections.Generic.Dictionary`2::_entries
	EntryU5BU5D_tEA0133B78B9FF7045128C508FA50247E525A94D6* ____entries_1;
	// System.Int32 System.Collections.Generic.Dictionary`2::_count
	int32_t ____count_2;
	// System.Int32 System.Collections.Generic.Dictionary`2::_freeList
	int32_t ____freeList_3;
	// System.Int32 System.Collections.Generic.Dictionary`2::_freeCount
	int32_t ____freeCount_4;
	// System.Int32 System.Collections.Generic.Dictionary`2::_version
	int32_t ____version_5;
	// System.Collections.Generic.IEqualityComparer`1<TKey> System.Collections.Generic.Dictionary`2::_comparer
	RuntimeObject* ____comparer_6;
	// System.Collections.Generic.Dictionary`2/KeyCollection<TKey,TValue> System.Collections.Generic.Dictionary`2::_keys
	KeyCollection_tCC15D033281A6593E2488FAF5B205812A152AC03* ____keys_7;
	// System.Collections.Generic.Dictionary`2/ValueCollection<TKey,TValue> System.Collections.Generic.Dictionary`2::_values
	ValueCollection_tCE6BD704B9571C131E2D8C8CED569DDEC4AE042B* ____values_8;
	// System.Object System.Collections.Generic.Dictionary`2::_syncRoot
	RuntimeObject* ____syncRoot_9;
};

// System.Collections.Generic.List`1<QFramework.AudioPlayer>
struct List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	AudioPlayerU5BU5D_t7846E47045539DC7F01D62590FB9608E0F1328D4* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

// System.Collections.Generic.List`1<QFramework.TimeExtend.Timer>
struct List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2  : public RuntimeObject
{
	// T[] System.Collections.Generic.List`1::_items
	TimerU5BU5D_t0388C5B52BCEDF995E3BE21CDA5048FB6317F5F5* ____items_1;
	// System.Int32 System.Collections.Generic.List`1::_size
	int32_t ____size_2;
	// System.Int32 System.Collections.Generic.List`1::_version
	int32_t ____version_3;
	// System.Object System.Collections.Generic.List`1::_syncRoot
	RuntimeObject* ____syncRoot_4;
};

// QFramework.Pool`1<QFramework.AudioPlayer>
struct Pool_1_t9A656BCB7186A2F154A9D9AAB28ED720232E5A8C  : public RuntimeObject
{
	// QFramework.IObjectFactory`1<T> QFramework.Pool`1::mFactory
	RuntimeObject* ___mFactory_0;
	// System.Collections.Generic.Stack`1<T> QFramework.Pool`1::mCacheStack
	Stack_1_t683E816BBF299A013486DA1C8015CC306FB4DECD* ___mCacheStack_1;
	// System.Int32 QFramework.Pool`1::mMaxCount
	int32_t ___mMaxCount_2;
};

// QFramework.Pool`1<QFramework.AudioSearchKeys>
struct Pool_1_t4E94A1D1E9289D3B38BBB9CE96528B26B2A1C257  : public RuntimeObject
{
	// QFramework.IObjectFactory`1<T> QFramework.Pool`1::mFactory
	RuntimeObject* ___mFactory_0;
	// System.Collections.Generic.Stack`1<T> QFramework.Pool`1::mCacheStack
	Stack_1_tEFFDDAE2E7445394E87DA99660C3DEB7C8FB6C20* ___mCacheStack_1;
	// System.Int32 QFramework.Pool`1::mMaxCount
	int32_t ___mMaxCount_2;
};

// QFramework.Pool`1<QFramework.FluentMusicAPI>
struct Pool_1_tD2EB9E09C41028DF67C3651728FB0E4D20EAFE6F  : public RuntimeObject
{
	// QFramework.IObjectFactory`1<T> QFramework.Pool`1::mFactory
	RuntimeObject* ___mFactory_0;
	// System.Collections.Generic.Stack`1<T> QFramework.Pool`1::mCacheStack
	Stack_1_t7754B9A71A1966949423FB645745741161E6823A* ___mCacheStack_1;
	// System.Int32 QFramework.Pool`1::mMaxCount
	int32_t ___mMaxCount_2;
};

// QFramework.Pool`1<QFramework.FluentSoundAPI>
struct Pool_1_tAFF21D5955206D4CDCB5B36B668F4485F01C0CFE  : public RuntimeObject
{
	// QFramework.IObjectFactory`1<T> QFramework.Pool`1::mFactory
	RuntimeObject* ___mFactory_0;
	// System.Collections.Generic.Stack`1<T> QFramework.Pool`1::mCacheStack
	Stack_1_t52C58EFF37E67FD5CD39872F5F118DF404F30BD7* ___mCacheStack_1;
	// System.Int32 QFramework.Pool`1::mMaxCount
	int32_t ___mMaxCount_2;
};

// QFramework.Pool`1<QFramework.TimeItem>
struct Pool_1_tBDE595BB71732B69BEE50EE63F07D573C57A5C0C  : public RuntimeObject
{
	// QFramework.IObjectFactory`1<T> QFramework.Pool`1::mFactory
	RuntimeObject* ___mFactory_0;
	// System.Collections.Generic.Stack`1<T> QFramework.Pool`1::mCacheStack
	Stack_1_t5AB6E9C738A9F2D44B2728233EDA003BA43495F1* ___mCacheStack_1;
	// System.Int32 QFramework.Pool`1::mMaxCount
	int32_t ___mMaxCount_2;
};

// QFramework.Property`1<System.Boolean>
struct Property_1_tCCE38EE3249FF8441B78437674A5542827A36708  : public RuntimeObject
{
	// System.Boolean QFramework.Property`1::mSetted
	bool ___mSetted_0;
	// T QFramework.Property`1::mValue
	bool ___mValue_1;
	// System.Action`1<T> QFramework.Property`1::mSetter
	Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* ___mSetter_2;
	// UnityEngine.Events.UnityEvent`1<T> QFramework.Property`1::OnValueChanged
	UnityEvent_1_tEEB36A367DCB5867E93AAF6BECAF3558CA71BECB* ___OnValueChanged_3;
};

// QFramework.Property`1<System.Int32>
struct Property_1_tEA2D0DDF24CE19765ADE39EFBB400F89850D7DA8  : public RuntimeObject
{
	// System.Boolean QFramework.Property`1::mSetted
	bool ___mSetted_0;
	// T QFramework.Property`1::mValue
	int32_t ___mValue_1;
	// System.Action`1<T> QFramework.Property`1::mSetter
	Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* ___mSetter_2;
	// UnityEngine.Events.UnityEvent`1<T> QFramework.Property`1::OnValueChanged
	UnityEvent_1_t7CC0661D6B113117B4CC68761D93AC8DF5DBD66A* ___OnValueChanged_3;
};

// System.Collections.Generic.Stack`1<QFramework.IAudioLoader>
struct Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5  : public RuntimeObject
{
	// T[] System.Collections.Generic.Stack`1::_array
	IAudioLoaderU5BU5D_t7651B2DEA9F10E6C148192FCFFB72DFD1528ED64* ____array_0;
	// System.Int32 System.Collections.Generic.Stack`1::_size
	int32_t ____size_1;
	// System.Int32 System.Collections.Generic.Stack`1::_version
	int32_t ____version_2;
	// System.Object System.Collections.Generic.Stack`1::_syncRoot
	RuntimeObject* ____syncRoot_3;
};

// System.Collections.Generic.Stack`1<System.Object>
struct Stack_1_tAD790A47551563636908E21E4F08C54C0C323EB5  : public RuntimeObject
{
	// T[] System.Collections.Generic.Stack`1::_array
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ____array_0;
	// System.Int32 System.Collections.Generic.Stack`1::_size
	int32_t ____size_1;
	// System.Int32 System.Collections.Generic.Stack`1::_version
	int32_t ____version_2;
	// System.Object System.Collections.Generic.Stack`1::_syncRoot
	RuntimeObject* ____syncRoot_3;
};

// QFramework.AbstractAudioLoaderPool
struct AbstractAudioLoaderPool_tA9514ECF9B0920914A41DA1E94F9C7914109136A  : public RuntimeObject
{
	// System.Collections.Generic.Stack`1<QFramework.IAudioLoader> QFramework.AbstractAudioLoaderPool::mPool
	Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5* ___mPool_0;
};

// QFramework.AudioKit
struct AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2  : public RuntimeObject
{
};

// QFramework.AudioKitConfig
struct AudioKitConfig_t0518724CE4F14A672627665BE74D533751686A54  : public RuntimeObject
{
	// QFramework.IAudioLoaderPool QFramework.AudioKitConfig::AudioLoaderPool
	RuntimeObject* ___AudioLoaderPool_0;
};

// QFramework.AudioKitOnFinishExtensions
struct AudioKitOnFinishExtensions_tAC8C4C51E2E8685DE2634F9A1985C8A439E54A22  : public RuntimeObject
{
};

// QFramework.AudioKitSettings
struct AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8  : public RuntimeObject
{
	// QFramework.PlayerPrefsBooleanProperty QFramework.AudioKitSettings::<IsSoundOn>k__BackingField
	PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* ___U3CIsSoundOnU3Ek__BackingField_6;
	// QFramework.PlayerPrefsBooleanProperty QFramework.AudioKitSettings::<IsMusicOn>k__BackingField
	PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* ___U3CIsMusicOnU3Ek__BackingField_7;
	// QFramework.PlayerPrefsBooleanProperty QFramework.AudioKitSettings::<IsVoiceOn>k__BackingField
	PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* ___U3CIsVoiceOnU3Ek__BackingField_8;
	// QFramework.PlayerPrefsFloatProperty QFramework.AudioKitSettings::<SoundVolume>k__BackingField
	PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* ___U3CSoundVolumeU3Ek__BackingField_9;
	// QFramework.PlayerPrefsFloatProperty QFramework.AudioKitSettings::<MusicVolume>k__BackingField
	PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* ___U3CMusicVolumeU3Ek__BackingField_10;
	// QFramework.PlayerPrefsFloatProperty QFramework.AudioKitSettings::<VoiceVolume>k__BackingField
	PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* ___U3CVoiceVolumeU3Ek__BackingField_11;
	// QFramework.CustomProperty`1<System.Boolean> QFramework.AudioKitSettings::<IsOn>k__BackingField
	CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A* ___U3CIsOnU3Ek__BackingField_12;
};

// QFramework.AudioPlayer
struct AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59  : public RuntimeObject
{
	// QFramework.IAudioLoader QFramework.AudioPlayer::mLoader
	RuntimeObject* ___mLoader_0;
	// UnityEngine.AudioSource QFramework.AudioPlayer::mAudioSource
	AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* ___mAudioSource_1;
	// System.String QFramework.AudioPlayer::mName
	String_t* ___mName_2;
	// QFramework.BindableProperty`1<System.Single> QFramework.AudioPlayer::<Volume>k__BackingField
	BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* ___U3CVolumeU3Ek__BackingField_3;
	// UnityEngine.AudioClip QFramework.AudioPlayer::mAudioClip
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___mAudioClip_4;
	// QFramework.TimeItem QFramework.AudioPlayer::mTimeItem
	TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* ___mTimeItem_5;
	// System.Action QFramework.AudioPlayer::mOnStart
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___mOnStart_6;
	// System.Action QFramework.AudioPlayer::mOnFinish
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___mOnFinish_7;
	// System.Boolean QFramework.AudioPlayer::mIsPause
	bool ___mIsPause_8;
	// System.Single QFramework.AudioPlayer::mLeftDelayTime
	float ___mLeftDelayTime_9;
	// System.Single QFramework.AudioPlayer::mVolumeScale
	float ___mVolumeScale_10;
	// System.Single QFramework.AudioPlayer::mVolume
	float ___mVolume_11;
	// System.Boolean QFramework.AudioPlayer::<UsedCache>k__BackingField
	bool ___U3CUsedCacheU3Ek__BackingField_12;
	// System.Boolean QFramework.AudioPlayer::<IsRecycled>k__BackingField
	bool ___U3CIsRecycledU3Ek__BackingField_13;
	// System.Boolean QFramework.AudioPlayer::<IsLoop>k__BackingField
	bool ___U3CIsLoopU3Ek__BackingField_14;
};

// QFramework.AudioSearchKeys
struct AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3  : public RuntimeObject
{
	// System.String QFramework.AudioSearchKeys::AssetBundleName
	String_t* ___AssetBundleName_0;
	// System.String QFramework.AudioSearchKeys::AssetName
	String_t* ___AssetName_1;
	// System.Boolean QFramework.AudioSearchKeys::<IsRecycled>k__BackingField
	bool ___U3CIsRecycledU3Ek__BackingField_2;
};

// QFramework.DefaultAudioLoader
struct DefaultAudioLoader_tA58E6CBC81292CF195A2FED8AF13ABF1473CF43F  : public RuntimeObject
{
	// UnityEngine.AudioClip QFramework.DefaultAudioLoader::mClip
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___mClip_0;
};

// QFramework.FluentMusicAPI
struct FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A  : public RuntimeObject
{
	// System.Boolean QFramework.FluentMusicAPI::<IsRecycled>k__BackingField
	bool ___U3CIsRecycledU3Ek__BackingField_0;
	// System.String QFramework.FluentMusicAPI::mName
	String_t* ___mName_1;
	// UnityEngine.AudioClip QFramework.FluentMusicAPI::mClip
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___mClip_2;
	// System.Boolean QFramework.FluentMusicAPI::mLoop
	bool ___mLoop_3;
	// System.Single QFramework.FluentMusicAPI::mVolumeScale
	float ___mVolumeScale_4;
};

// QFramework.FluentSoundAPI
struct FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C  : public RuntimeObject
{
	// System.Boolean QFramework.FluentSoundAPI::<IsRecycled>k__BackingField
	bool ___U3CIsRecycledU3Ek__BackingField_0;
	// System.String QFramework.FluentSoundAPI::mName
	String_t* ___mName_1;
	// UnityEngine.AudioClip QFramework.FluentSoundAPI::mClip
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___mClip_2;
	// System.Boolean QFramework.FluentSoundAPI::mLoop
	bool ___mLoop_3;
	// System.Single QFramework.FluentSoundAPI::mVolumeScale
	float ___mVolumeScale_4;
};

// QFramework.PlaySoundExtension
struct PlaySoundExtension_t482737207F3461A157EDAE9CC5E6F4BAA68744E2  : public RuntimeObject
{
};

// System.String
struct String_t  : public RuntimeObject
{
	// System.Int32 System.String::_stringLength
	int32_t ____stringLength_4;
	// System.Char System.String::_firstChar
	Il2CppChar ____firstChar_5;
};

// QFramework.TimeItem
struct TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8  : public RuntimeObject
{
	// System.Single QFramework.TimeItem::mDelayTime
	float ___mDelayTime_0;
	// System.Int32 QFramework.TimeItem::mRepeatCount
	int32_t ___mRepeatCount_1;
	// System.Action`1<System.Int32> QFramework.TimeItem::mCallback
	Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* ___mCallback_2;
	// System.Int32 QFramework.TimeItem::mCallbackTick
	int32_t ___mCallbackTick_3;
	// System.Single QFramework.TimeItem::<SortScore>k__BackingField
	float ___U3CSortScoreU3Ek__BackingField_4;
	// System.Int32 QFramework.TimeItem::<HeapIndex>k__BackingField
	int32_t ___U3CHeapIndexU3Ek__BackingField_5;
	// System.Boolean QFramework.TimeItem::<IsEnable>k__BackingField
	bool ___U3CIsEnableU3Ek__BackingField_6;
	// System.Boolean QFramework.TimeItem::<IsRecycled>k__BackingField
	bool ___U3CIsRecycledU3Ek__BackingField_7;
};

// QFramework.TimeExtend.Timer
struct Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4  : public RuntimeObject
{
	// System.Action`1<System.Single> QFramework.TimeExtend.Timer::UpdateEvent
	Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* ___UpdateEvent_1;
	// System.Action QFramework.TimeExtend.Timer::EndEvent
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___EndEvent_2;
	// System.Single QFramework.TimeExtend.Timer::mTime
	float ___mTime_3;
	// System.Boolean QFramework.TimeExtend.Timer::mLoop
	bool ___mLoop_4;
	// System.Boolean QFramework.TimeExtend.Timer::mIgnorTimescale
	bool ___mIgnorTimescale_5;
	// System.String QFramework.TimeExtend.Timer::mFlag
	String_t* ___mFlag_6;
	// System.Single QFramework.TimeExtend.Timer::mCachedTime
	float ___mCachedTime_8;
	// System.Single QFramework.TimeExtend.Timer::mTimePassed
	float ___mTimePassed_9;
	// System.Boolean QFramework.TimeExtend.Timer::mIsFinish
	bool ___mIsFinish_10;
	// System.Boolean QFramework.TimeExtend.Timer::mIsPause
	bool ___mIsPause_11;
};

// System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.ValueType
struct ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F_marshaled_com
{
};

// UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D  : public RuntimeObject
{
};
// Native definition for P/Invoke marshalling of UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
};
// Native definition for COM marshalling of UnityEngine.YieldInstruction
struct YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
};

// QFramework.AudioKit/<>c
struct U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC  : public RuntimeObject
{
};

// QFramework.AudioKit/<>c__DisplayClass21_0
struct U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157  : public RuntimeObject
{
	// System.Action`1<QFramework.AudioPlayer> QFramework.AudioKit/<>c__DisplayClass21_0::callBack
	Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* ___callBack_0;
	// QFramework.AudioPlayer QFramework.AudioKit/<>c__DisplayClass21_0::soundPlayer
	AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___soundPlayer_1;
	// System.String QFramework.AudioKit/<>c__DisplayClass21_0::soundName
	String_t* ___soundName_2;
};

// QFramework.AudioKit/<>c__DisplayClass25_0
struct U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51  : public RuntimeObject
{
	// System.Action`1<QFramework.AudioPlayer> QFramework.AudioKit/<>c__DisplayClass25_0::callBack
	Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* ___callBack_0;
	// QFramework.AudioPlayer QFramework.AudioKit/<>c__DisplayClass25_0::soundPlayer
	AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___soundPlayer_1;
};

// QFramework.AudioManager/<>c
struct U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86  : public RuntimeObject
{
};

// QFramework.DefaultAudioLoader/<>c__DisplayClass4_0
struct U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B  : public RuntimeObject
{
	// UnityEngine.ResourceRequest QFramework.DefaultAudioLoader/<>c__DisplayClass4_0::resourceRequest
	ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868* ___resourceRequest_0;
	// System.Action`2<System.Boolean,UnityEngine.AudioClip> QFramework.DefaultAudioLoader/<>c__DisplayClass4_0::onLoad
	Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD* ___onLoad_1;
};

// QFramework.TimeExtend.Timer/<>c__DisplayClass15_0
struct U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA  : public RuntimeObject
{
	// System.String QFramework.TimeExtend.Timer/<>c__DisplayClass15_0::flag
	String_t* ___flag_0;
};

// QFramework.CustomProperty`1<System.Boolean>
struct CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A  : public Property_1_tCCE38EE3249FF8441B78437674A5542827A36708
{
	// System.Func`1<T> QFramework.CustomProperty`1::mValueGetter
	Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457* ___mValueGetter_4;
	// System.Action`1<T> QFramework.CustomProperty`1::mValueSetter
	Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* ___mValueSetter_5;
};

// System.Collections.Generic.KeyValuePair`2<System.Object,System.Object>
struct KeyValuePair_2_tFC32D2507216293851350D29B64D79F950B55230 
{
	// TKey System.Collections.Generic.KeyValuePair`2::key
	RuntimeObject* ___key_0;
	// TValue System.Collections.Generic.KeyValuePair`2::value
	RuntimeObject* ___value_1;
};

// System.Collections.Generic.KeyValuePair`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>
struct KeyValuePair_2_tA1ACD899BEC543AE9704AB31AC4C62160B215F43 
{
	// TKey System.Collections.Generic.KeyValuePair`2::key
	String_t* ___key_0;
	// TValue System.Collections.Generic.KeyValuePair`2::value
	List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* ___value_1;
};

// QFramework.SafeObjectPool`1<QFramework.AudioPlayer>
struct SafeObjectPool_1_t3542EC4AA4871EFF26264DA85DCDB6C39D22A8AD  : public Pool_1_t9A656BCB7186A2F154A9D9AAB28ED720232E5A8C
{
};

// QFramework.SafeObjectPool`1<QFramework.AudioSearchKeys>
struct SafeObjectPool_1_t7D55BDF522ED56ECFF3CBDB6115923D35053E535  : public Pool_1_t4E94A1D1E9289D3B38BBB9CE96528B26B2A1C257
{
};

// QFramework.SafeObjectPool`1<QFramework.FluentMusicAPI>
struct SafeObjectPool_1_t33C1137434D07D7A43A1A8C7D46F1C04B665E6F0  : public Pool_1_tD2EB9E09C41028DF67C3651728FB0E4D20EAFE6F
{
};

// QFramework.SafeObjectPool`1<QFramework.FluentSoundAPI>
struct SafeObjectPool_1_t7FA207C564A34D5D5E4DE789675614FD0D0964A2  : public Pool_1_tAFF21D5955206D4CDCB5B36B668F4485F01C0CFE
{
};

// QFramework.SafeObjectPool`1<QFramework.TimeItem>
struct SafeObjectPool_1_tB9A3A3095B574142CF0139B7323BA66C80DCC342  : public Pool_1_tBDE595BB71732B69BEE50EE63F07D573C57A5C0C
{
};

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22 
{
	// System.Boolean System.Boolean::m_value
	bool ___m_value_0;
};

// QFramework.DefaultAudioLoaderPool
struct DefaultAudioLoaderPool_t83CA69E29CA127237BADCBB67697143A0ED7D53A  : public AbstractAudioLoaderPool_tA9514ECF9B0920914A41DA1E94F9C7914109136A
{
};

// System.Enum
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2  : public ValueType_t6D9B272BD21782F0A9A14F2E41F85A50E97A986F
{
};
// Native definition for P/Invoke marshalling of System.Enum
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_pinvoke
{
};
// Native definition for COM marshalling of System.Enum
struct Enum_t2A1A94B24E3B776EEF4E5E485E290BB9D4D072E2_marshaled_com
{
};

// System.Int32
struct Int32_t680FF22E76F6EFAD4375103CBBFFA0421349384C 
{
	// System.Int32 System.Int32::m_value
	int32_t ___m_value_0;
};

// QFramework.IntProperty
struct IntProperty_t4E5B676427BABBB583A5A88D57DA2C0BCF45A10A  : public Property_1_tEA2D0DDF24CE19765ADE39EFBB400F89850D7DA8
{
};

// System.IntPtr
struct IntPtr_t 
{
	// System.Void* System.IntPtr::m_value
	void* ___m_value_0;
};

// QFramework.PlayerPrefsBooleanProperty
struct PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3  : public BindableProperty_1_t1ACEAE0BD8CBD20B51A6CACA4F4B56BE6F680D8A
{
};

// QFramework.PlayerPrefsFloatProperty
struct PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793  : public BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58
{
};

// System.Single
struct Single_t4530F2FF86FCB0DC29F35385CA1BD21BE294761C 
{
	// System.Single System.Single::m_value
	float ___m_value_0;
};

// UnityEngine.Vector3
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 
{
	// System.Single UnityEngine.Vector3::x
	float ___x_2;
	// System.Single UnityEngine.Vector3::y
	float ___y_3;
	// System.Single UnityEngine.Vector3::z
	float ___z_4;
};

// System.Void
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

// QFramework.ActionStatus
struct ActionStatus_t1327F3D7604043535EB254069854E0E63753711C 
{
	// System.Int32 QFramework.ActionStatus::value__
	int32_t ___value___2;
};

// UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C  : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D
{
	// System.IntPtr UnityEngine.AsyncOperation::m_Ptr
	intptr_t ___m_Ptr_0;
	// System.Action`1<UnityEngine.AsyncOperation> UnityEngine.AsyncOperation::m_completeCallback
	Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB* ___m_completeCallback_1;
};
// Native definition for P/Invoke marshalling of UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_pinvoke : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_pinvoke
{
	intptr_t ___m_Ptr_0;
	Il2CppMethodPointer ___m_completeCallback_1;
};
// Native definition for COM marshalling of UnityEngine.AsyncOperation
struct AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_com : public YieldInstruction_tFCE35FD0907950EFEE9BC2890AC664E41C53728D_marshaled_com
{
	intptr_t ___m_Ptr_0;
	Il2CppMethodPointer ___m_completeCallback_1;
};

// QFramework.BinaryHeapBuildMode
struct BinaryHeapBuildMode_t315F096B5FB8B77DFAA8605FCAE3AC805F2BCE51 
{
	// System.Int32 QFramework.BinaryHeapBuildMode::value__
	int32_t ___value___2;
};

// QFramework.BinaryHeapSortMode
struct BinaryHeapSortMode_t4C7D06E525B5F3D54FA97CE6698A4DF7BB5A1426 
{
	// System.Int32 QFramework.BinaryHeapSortMode::value__
	int32_t ___value___2;
};

// System.Delegate
struct Delegate_t  : public RuntimeObject
{
	// System.IntPtr System.Delegate::method_ptr
	intptr_t ___method_ptr_0;
	// System.IntPtr System.Delegate::invoke_impl
	intptr_t ___invoke_impl_1;
	// System.Object System.Delegate::m_target
	RuntimeObject* ___m_target_2;
	// System.IntPtr System.Delegate::method
	intptr_t ___method_3;
	// System.IntPtr System.Delegate::delegate_trampoline
	intptr_t ___delegate_trampoline_4;
	// System.IntPtr System.Delegate::extra_arg
	intptr_t ___extra_arg_5;
	// System.IntPtr System.Delegate::method_code
	intptr_t ___method_code_6;
	// System.IntPtr System.Delegate::interp_method
	intptr_t ___interp_method_7;
	// System.IntPtr System.Delegate::interp_invoke_impl
	intptr_t ___interp_invoke_impl_8;
	// System.Reflection.MethodInfo System.Delegate::method_info
	MethodInfo_t* ___method_info_9;
	// System.Reflection.MethodInfo System.Delegate::original_method_info
	MethodInfo_t* ___original_method_info_10;
	// System.DelegateData System.Delegate::data
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	// System.Boolean System.Delegate::method_is_virtual
	bool ___method_is_virtual_12;
};
// Native definition for P/Invoke marshalling of System.Delegate
struct Delegate_t_marshaled_pinvoke
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};
// Native definition for COM marshalling of System.Delegate
struct Delegate_t_marshaled_com
{
	intptr_t ___method_ptr_0;
	intptr_t ___invoke_impl_1;
	Il2CppIUnknown* ___m_target_2;
	intptr_t ___method_3;
	intptr_t ___delegate_trampoline_4;
	intptr_t ___extra_arg_5;
	intptr_t ___method_code_6;
	intptr_t ___interp_method_7;
	intptr_t ___interp_invoke_impl_8;
	MethodInfo_t* ___method_info_9;
	MethodInfo_t* ___original_method_info_10;
	DelegateData_t9B286B493293CD2D23A5B2B5EF0E5B1324C2B77E* ___data_11;
	int32_t ___method_is_virtual_12;
};

// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C  : public RuntimeObject
{
	// System.IntPtr UnityEngine.Object::m_CachedPtr
	intptr_t ___m_CachedPtr_0;
};
// Native definition for P/Invoke marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_pinvoke
{
	intptr_t ___m_CachedPtr_0;
};
// Native definition for COM marshalling of UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_marshaled_com
{
	intptr_t ___m_CachedPtr_0;
};

// QFramework.AudioKit/PlaySoundModes
struct PlaySoundModes_t8FDA95E0834F9306E9BDFC8240E8D7EECEBB187F 
{
	// System.Int32 QFramework.AudioKit/PlaySoundModes::value__
	int32_t ___value___2;
};

// QFramework.PlaySoundAction/Modes
struct Modes_tC28A291AD8F588DF5193A6889C1DC26BA8D4CF18 
{
	// System.Int32 QFramework.PlaySoundAction/Modes::value__
	int32_t ___value___2;
};

// QFramework.AbstractAction`1<QFramework.PlaySoundAction>
struct AbstractAction_1_t9A1EECE1BD7DD95509513E65E672298D45093CD3  : public RuntimeObject
{
	// System.UInt64 QFramework.AbstractAction`1::<ActionID>k__BackingField
	uint64_t ___U3CActionIDU3Ek__BackingField_1;
	// QFramework.ActionStatus QFramework.AbstractAction`1::<Status>k__BackingField
	int32_t ___U3CStatusU3Ek__BackingField_2;
	// System.Boolean QFramework.AbstractAction`1::<Paused>k__BackingField
	bool ___U3CPausedU3Ek__BackingField_3;
	// System.Boolean QFramework.AbstractAction`1::<Deinited>k__BackingField
	bool ___U3CDeinitedU3Ek__BackingField_4;
};

// QFramework.BinaryHeap`1<QFramework.TimeItem>
struct BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03  : public RuntimeObject
{
	// T[] QFramework.BinaryHeap`1::mArray
	TimeItemU5BU5D_t8144BE62A6EE673DF12BBA5A1F8826696EBD58D1* ___mArray_0;
	// System.Single QFramework.BinaryHeap`1::mGrowthFactor
	float ___mGrowthFactor_1;
	// System.Int32 QFramework.BinaryHeap`1::mLastChildIndex
	int32_t ___mLastChildIndex_2;
	// QFramework.BinaryHeapSortMode QFramework.BinaryHeap`1::mSortMode
	int32_t ___mSortMode_3;
};

// UnityEngine.AudioClip
struct AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
	// UnityEngine.AudioClip/PCMReaderCallback UnityEngine.AudioClip::m_PCMReaderCallback
	PCMReaderCallback_t3396D9613664F0AFF65FB91018FD0F901CC16F1E* ___m_PCMReaderCallback_4;
	// UnityEngine.AudioClip/PCMSetPositionCallback UnityEngine.AudioClip::m_PCMSetPositionCallback
	PCMSetPositionCallback_t8D7135A2FB40647CAEC93F5254AD59E18DEB6072* ___m_PCMSetPositionCallback_5;
};

// UnityEngine.Component
struct Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// UnityEngine.GameObject
struct GameObject_t76FEDD663AB33C991A9C9A23129337651094216F  : public Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C
{
};

// System.MulticastDelegate
struct MulticastDelegate_t  : public Delegate_t
{
	// System.Delegate[] System.MulticastDelegate::delegates
	DelegateU5BU5D_tC5AB7E8F745616680F337909D3A8E6C722CDF771* ___delegates_13;
};
// Native definition for P/Invoke marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_pinvoke : public Delegate_t_marshaled_pinvoke
{
	Delegate_t_marshaled_pinvoke** ___delegates_13;
};
// Native definition for COM marshalling of System.MulticastDelegate
struct MulticastDelegate_t_marshaled_com : public Delegate_t_marshaled_com
{
	Delegate_t_marshaled_com** ___delegates_13;
};

// UnityEngine.ResourceRequest
struct ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868  : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C
{
	// System.String UnityEngine.ResourceRequest::m_Path
	String_t* ___m_Path_2;
	// System.Type UnityEngine.ResourceRequest::m_Type
	Type_t* ___m_Type_3;
};
// Native definition for P/Invoke marshalling of UnityEngine.ResourceRequest
struct ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868_marshaled_pinvoke : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_pinvoke
{
	char* ___m_Path_2;
	Type_t* ___m_Type_3;
};
// Native definition for COM marshalling of UnityEngine.ResourceRequest
struct ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868_marshaled_com : public AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C_marshaled_com
{
	Il2CppChar* ___m_Path_2;
	Type_t* ___m_Type_3;
};

// System.Action`1<UnityEngine.AsyncOperation>
struct Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB  : public MulticastDelegate_t
{
};

// System.Action`1<QFramework.AudioPlayer>
struct Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219  : public MulticastDelegate_t
{
};

// System.Action`1<System.Boolean>
struct Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C  : public MulticastDelegate_t
{
};

// System.Action`1<System.Int32>
struct Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404  : public MulticastDelegate_t
{
};

// System.Action`1<System.Object>
struct Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87  : public MulticastDelegate_t
{
};

// System.Action`1<System.Single>
struct Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A  : public MulticastDelegate_t
{
};

// System.Action`2<System.Boolean,UnityEngine.AudioClip>
struct Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD  : public MulticastDelegate_t
{
};

// System.Action`2<System.Boolean,System.Object>
struct Action_2_t4E94B0FCA1084D7868DB11A50767A4916CA3D3FB  : public MulticastDelegate_t
{
};

// System.Func`1<System.Boolean>
struct Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457  : public MulticastDelegate_t
{
};

// System.Func`2<System.Collections.Generic.KeyValuePair`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>,System.Collections.Generic.IEnumerable`1<QFramework.AudioPlayer>>
struct Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB  : public MulticastDelegate_t
{
};

// System.Predicate`1<QFramework.TimeExtend.Timer>
struct Predicate_1_t55DAAC67F486E666A2115F381B43C27ADF0B01CF  : public MulticastDelegate_t
{
};

// System.Action
struct Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07  : public MulticastDelegate_t
{
};

// UnityEngine.Behaviour
struct Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// QFramework.PlaySoundAction
struct PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909  : public AbstractAction_1_t9A1EECE1BD7DD95509513E65E672298D45093CD3
{
	// QFramework.PlaySoundAction/Modes QFramework.PlaySoundAction::mMode
	int32_t ___mMode_5;
	// System.String QFramework.PlaySoundAction::mSoundName
	String_t* ___mSoundName_6;
	// UnityEngine.AudioClip QFramework.PlaySoundAction::mAudioClip
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___mAudioClip_7;
	// System.Action QFramework.PlaySoundAction::mOnFinish
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___mOnFinish_8;
};

// UnityEngine.Transform
struct Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1  : public Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3
{
};

// UnityEngine.AudioBehaviour
struct AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};

// UnityEngine.MonoBehaviour
struct MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71  : public Behaviour_t01970CFBBA658497AE30F311C447DB0440BAB7FA
{
};

// UnityEngine.AudioListener
struct AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35  : public AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941
{
};

// QFramework.AudioManager
struct AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// QFramework.AudioPlayer QFramework.AudioManager::<MusicPlayer>k__BackingField
	AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___U3CMusicPlayerU3Ek__BackingField_4;
	// QFramework.AudioPlayer QFramework.AudioManager::<VoicePlayer>k__BackingField
	AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___U3CVoicePlayerU3Ek__BackingField_5;
	// UnityEngine.AudioListener QFramework.AudioManager::mAudioListener
	AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35* ___mAudioListener_7;
	// System.String QFramework.AudioManager::<CurrentMusicName>k__BackingField
	String_t* ___U3CCurrentMusicNameU3Ek__BackingField_8;
	// System.String QFramework.AudioManager::<CurrentVoiceName>k__BackingField
	String_t* ___U3CCurrentVoiceNameU3Ek__BackingField_9;
};

// UnityEngine.AudioSource
struct AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299  : public AudioBehaviour_t2DC0BEF7B020C952F3D2DA5AAAC88501C7EEB941
{
};

// QFramework.Timer
struct Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
	// QFramework.BinaryHeap`1<QFramework.TimeItem> QFramework.Timer::mUnScaleTimeHeap
	BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* ___mUnScaleTimeHeap_4;
	// QFramework.BinaryHeap`1<QFramework.TimeItem> QFramework.Timer::mScaleTimeHeap
	BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* ___mScaleTimeHeap_5;
	// System.Single QFramework.Timer::mCurrentUnScaleTime
	float ___mCurrentUnScaleTime_6;
	// System.Single QFramework.Timer::<CurrentScaleTime>k__BackingField
	float ___U3CCurrentScaleTimeU3Ek__BackingField_7;
};

// QFramework.TimeExtend.TimerDriver
struct TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2  : public MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71
{
};

// <Module>

// <Module>

// QFramework.BindableProperty`1<System.Boolean>
struct BindableProperty_1_t1ACEAE0BD8CBD20B51A6CACA4F4B56BE6F680D8A_StaticFields
{
	// System.Func`3<T,T,System.Boolean> QFramework.BindableProperty`1::<Comparer>k__BackingField
	Func_3_t8405246FA4174D01D5F07F8A51737739EE5376F3* ___U3CComparerU3Ek__BackingField_1;
};

// QFramework.BindableProperty`1<System.Boolean>

// QFramework.BindableProperty`1<System.Single>
struct BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58_StaticFields
{
	// System.Func`3<T,T,System.Boolean> QFramework.BindableProperty`1::<Comparer>k__BackingField
	Func_3_tA9AA477D8A5A68C7DC26AE4792295B80F920E61E* ___U3CComparerU3Ek__BackingField_1;
};

// QFramework.BindableProperty`1<System.Single>

// System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>

// System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>

// System.Collections.Generic.Dictionary`2<System.String,System.Int32>

// System.Collections.Generic.Dictionary`2<System.String,System.Int32>

// System.Collections.Generic.List`1<QFramework.AudioPlayer>
struct List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	AudioPlayerU5BU5D_t7846E47045539DC7F01D62590FB9608E0F1328D4* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<QFramework.AudioPlayer>

// System.Collections.Generic.List`1<System.Object>
struct List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<System.Object>

// System.Collections.Generic.List`1<QFramework.TimeExtend.Timer>
struct List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2_StaticFields
{
	// T[] System.Collections.Generic.List`1::s_emptyArray
	TimerU5BU5D_t0388C5B52BCEDF995E3BE21CDA5048FB6317F5F5* ___s_emptyArray_5;
};

// System.Collections.Generic.List`1<QFramework.TimeExtend.Timer>

// QFramework.Pool`1<QFramework.AudioPlayer>

// QFramework.Pool`1<QFramework.AudioPlayer>

// QFramework.Pool`1<QFramework.AudioSearchKeys>

// QFramework.Pool`1<QFramework.AudioSearchKeys>

// QFramework.Pool`1<QFramework.FluentMusicAPI>

// QFramework.Pool`1<QFramework.FluentMusicAPI>

// QFramework.Pool`1<QFramework.FluentSoundAPI>

// QFramework.Pool`1<QFramework.FluentSoundAPI>

// QFramework.Pool`1<QFramework.TimeItem>

// QFramework.Pool`1<QFramework.TimeItem>

// QFramework.Property`1<System.Int32>

// QFramework.Property`1<System.Int32>

// System.Collections.Generic.Stack`1<QFramework.IAudioLoader>

// System.Collections.Generic.Stack`1<QFramework.IAudioLoader>

// System.Collections.Generic.Stack`1<System.Object>

// System.Collections.Generic.Stack`1<System.Object>

// QFramework.AbstractAudioLoaderPool

// QFramework.AbstractAudioLoaderPool

// QFramework.AudioKit
struct AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields
{
	// QFramework.AudioKitConfig QFramework.AudioKit::Config
	AudioKitConfig_t0518724CE4F14A672627665BE74D533751686A54* ___Config_0;
	// QFramework.AudioKit/PlaySoundModes QFramework.AudioKit::PlaySoundMode
	int32_t ___PlaySoundMode_1;
	// System.Int32 QFramework.AudioKit::SoundFrameCountForIgnoreSameSound
	int32_t ___SoundFrameCountForIgnoreSameSound_2;
	// System.Int32 QFramework.AudioKit::GlobalFrameCountForIgnoreSameSound
	int32_t ___GlobalFrameCountForIgnoreSameSound_3;
	// System.Collections.Generic.Dictionary`2<System.String,System.Int32> QFramework.AudioKit::mSoundFrameCountForName
	Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* ___mSoundFrameCountForName_4;
	// System.Int32 QFramework.AudioKit::mGlobalFrameCount
	int32_t ___mGlobalFrameCount_5;
	// QFramework.AudioKitSettings QFramework.AudioKit::<Settings>k__BackingField
	AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* ___U3CSettingsU3Ek__BackingField_6;
};

// QFramework.AudioKit

// QFramework.AudioKitConfig

// QFramework.AudioKitConfig

// QFramework.AudioKitOnFinishExtensions

// QFramework.AudioKitOnFinishExtensions

// QFramework.AudioKitSettings

// QFramework.AudioKitSettings

// QFramework.AudioPlayer

// QFramework.AudioPlayer

// QFramework.AudioSearchKeys

// QFramework.AudioSearchKeys

// QFramework.DefaultAudioLoader

// QFramework.DefaultAudioLoader

// QFramework.FluentMusicAPI

// QFramework.FluentMusicAPI

// QFramework.FluentSoundAPI

// QFramework.FluentSoundAPI

// QFramework.PlaySoundExtension

// QFramework.PlaySoundExtension

// System.String
struct String_t_StaticFields
{
	// System.String System.String::Empty
	String_t* ___Empty_6;
};

// System.String

// QFramework.TimeItem

// QFramework.TimeItem

// QFramework.TimeExtend.Timer
struct Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields
{
	// System.Collections.Generic.List`1<QFramework.TimeExtend.Timer> QFramework.TimeExtend.Timer::timers
	List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* ___timers_0;
	// QFramework.TimeExtend.TimerDriver QFramework.TimeExtend.Timer::Driver
	TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* ___Driver_7;
};

// QFramework.TimeExtend.Timer

// QFramework.AudioKit/<>c
struct U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_StaticFields
{
	// QFramework.AudioKit/<>c QFramework.AudioKit/<>c::<>9
	U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC* ___U3CU3E9_0;
	// System.Action`1<QFramework.AudioPlayer> QFramework.AudioKit/<>c::<>9__22_0
	Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* ___U3CU3E9__22_0_1;
};

// QFramework.AudioKit/<>c

// QFramework.AudioKit/<>c__DisplayClass21_0

// QFramework.AudioKit/<>c__DisplayClass21_0

// QFramework.AudioKit/<>c__DisplayClass25_0

// QFramework.AudioKit/<>c__DisplayClass25_0

// QFramework.AudioManager/<>c
struct U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_StaticFields
{
	// QFramework.AudioManager/<>c QFramework.AudioManager/<>c::<>9
	U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86* ___U3CU3E9_0;
	// System.Action`1<QFramework.AudioPlayer> QFramework.AudioManager/<>c::<>9__8_3
	Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* ___U3CU3E9__8_3_1;
	// System.Func`2<System.Collections.Generic.KeyValuePair`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>,System.Collections.Generic.IEnumerable`1<QFramework.AudioPlayer>> QFramework.AudioManager/<>c::<>9__10_0
	Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB* ___U3CU3E9__10_0_2;
};

// QFramework.AudioManager/<>c

// QFramework.DefaultAudioLoader/<>c__DisplayClass4_0

// QFramework.DefaultAudioLoader/<>c__DisplayClass4_0

// QFramework.TimeExtend.Timer/<>c__DisplayClass15_0

// QFramework.TimeExtend.Timer/<>c__DisplayClass15_0

// QFramework.CustomProperty`1<System.Boolean>

// QFramework.CustomProperty`1<System.Boolean>

// System.Collections.Generic.KeyValuePair`2<System.Object,System.Object>

// System.Collections.Generic.KeyValuePair`2<System.Object,System.Object>

// System.Collections.Generic.KeyValuePair`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>

// System.Collections.Generic.KeyValuePair`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>

// QFramework.SafeObjectPool`1<QFramework.AudioPlayer>

// QFramework.SafeObjectPool`1<QFramework.AudioPlayer>

// QFramework.SafeObjectPool`1<QFramework.AudioSearchKeys>

// QFramework.SafeObjectPool`1<QFramework.AudioSearchKeys>

// QFramework.SafeObjectPool`1<QFramework.FluentMusicAPI>

// QFramework.SafeObjectPool`1<QFramework.FluentMusicAPI>

// QFramework.SafeObjectPool`1<QFramework.FluentSoundAPI>

// QFramework.SafeObjectPool`1<QFramework.FluentSoundAPI>

// QFramework.SafeObjectPool`1<QFramework.TimeItem>

// QFramework.SafeObjectPool`1<QFramework.TimeItem>

// System.Boolean
struct Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_StaticFields
{
	// System.String System.Boolean::TrueString
	String_t* ___TrueString_5;
	// System.String System.Boolean::FalseString
	String_t* ___FalseString_6;
};

// System.Boolean

// QFramework.DefaultAudioLoaderPool

// QFramework.DefaultAudioLoaderPool

// System.Int32

// System.Int32

// QFramework.IntProperty

// QFramework.IntProperty

// System.IntPtr
struct IntPtr_t_StaticFields
{
	// System.IntPtr System.IntPtr::Zero
	intptr_t ___Zero_1;
};

// System.IntPtr

// QFramework.PlayerPrefsBooleanProperty

// QFramework.PlayerPrefsBooleanProperty

// QFramework.PlayerPrefsFloatProperty

// QFramework.PlayerPrefsFloatProperty

// System.Single

// System.Single

// UnityEngine.Vector3
struct Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields
{
	// UnityEngine.Vector3 UnityEngine.Vector3::zeroVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___zeroVector_5;
	// UnityEngine.Vector3 UnityEngine.Vector3::oneVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___oneVector_6;
	// UnityEngine.Vector3 UnityEngine.Vector3::upVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___upVector_7;
	// UnityEngine.Vector3 UnityEngine.Vector3::downVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___downVector_8;
	// UnityEngine.Vector3 UnityEngine.Vector3::leftVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___leftVector_9;
	// UnityEngine.Vector3 UnityEngine.Vector3::rightVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___rightVector_10;
	// UnityEngine.Vector3 UnityEngine.Vector3::forwardVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___forwardVector_11;
	// UnityEngine.Vector3 UnityEngine.Vector3::backVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___backVector_12;
	// UnityEngine.Vector3 UnityEngine.Vector3::positiveInfinityVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___positiveInfinityVector_13;
	// UnityEngine.Vector3 UnityEngine.Vector3::negativeInfinityVector
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___negativeInfinityVector_14;
};

// UnityEngine.Vector3

// System.Void

// System.Void

// UnityEngine.AsyncOperation

// UnityEngine.AsyncOperation

// QFramework.BinaryHeapBuildMode

// QFramework.BinaryHeapBuildMode

// QFramework.BinaryHeapSortMode

// QFramework.BinaryHeapSortMode

// System.Delegate

// System.Delegate

// UnityEngine.Object
struct Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_StaticFields
{
	// System.Int32 UnityEngine.Object::OffsetOfInstanceIDInCPlusPlusObject
	int32_t ___OffsetOfInstanceIDInCPlusPlusObject_1;
};

// UnityEngine.Object

// QFramework.AudioKit/PlaySoundModes

// QFramework.AudioKit/PlaySoundModes

// QFramework.PlaySoundAction/Modes

// QFramework.PlaySoundAction/Modes

// QFramework.AbstractAction`1<QFramework.PlaySoundAction>
struct AbstractAction_1_t9A1EECE1BD7DD95509513E65E672298D45093CD3_StaticFields
{
	// QFramework.SimpleObjectPool`1<T> QFramework.AbstractAction`1::mPool
	SimpleObjectPool_1_t708F067E82816266FD6BDBC4567EE7E93D476A88* ___mPool_0;
};

// QFramework.AbstractAction`1<QFramework.PlaySoundAction>

// QFramework.BinaryHeap`1<QFramework.TimeItem>

// QFramework.BinaryHeap`1<QFramework.TimeItem>

// UnityEngine.AudioClip

// UnityEngine.AudioClip

// UnityEngine.Component

// UnityEngine.Component

// UnityEngine.GameObject

// UnityEngine.GameObject

// UnityEngine.ResourceRequest

// UnityEngine.ResourceRequest

// System.Action`1<UnityEngine.AsyncOperation>

// System.Action`1<UnityEngine.AsyncOperation>

// System.Action`1<QFramework.AudioPlayer>

// System.Action`1<QFramework.AudioPlayer>

// System.Action`1<System.Boolean>

// System.Action`1<System.Boolean>

// System.Action`1<System.Int32>

// System.Action`1<System.Int32>

// System.Action`1<System.Object>

// System.Action`1<System.Object>

// System.Action`1<System.Single>

// System.Action`1<System.Single>

// System.Action`2<System.Boolean,UnityEngine.AudioClip>

// System.Action`2<System.Boolean,UnityEngine.AudioClip>

// System.Action`2<System.Boolean,System.Object>

// System.Action`2<System.Boolean,System.Object>

// System.Func`1<System.Boolean>

// System.Func`1<System.Boolean>

// System.Func`2<System.Collections.Generic.KeyValuePair`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>,System.Collections.Generic.IEnumerable`1<QFramework.AudioPlayer>>

// System.Func`2<System.Collections.Generic.KeyValuePair`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>,System.Collections.Generic.IEnumerable`1<QFramework.AudioPlayer>>

// System.Predicate`1<QFramework.TimeExtend.Timer>

// System.Predicate`1<QFramework.TimeExtend.Timer>

// System.Action

// System.Action

// QFramework.PlaySoundAction

// QFramework.PlaySoundAction

// UnityEngine.Transform

// UnityEngine.Transform

// UnityEngine.MonoBehaviour

// UnityEngine.MonoBehaviour

// UnityEngine.AudioListener

// UnityEngine.AudioListener

// QFramework.AudioManager
struct AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_StaticFields
{
	// System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>> QFramework.AudioManager::mSoundPlayerInPlaying
	Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* ___mSoundPlayerInPlaying_6;
};

// QFramework.AudioManager

// UnityEngine.AudioSource

// UnityEngine.AudioSource

// QFramework.Timer

// QFramework.Timer

// QFramework.TimeExtend.TimerDriver
struct TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_StaticFields
{
	// QFramework.TimeExtend.TimerDriver QFramework.TimeExtend.TimerDriver::mInstance
	TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* ___mInstance_4;
};

// QFramework.TimeExtend.TimerDriver
#ifdef __clang__
#pragma clang diagnostic pop
#endif
// System.Object[]
struct ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918  : public RuntimeArray
{
	ALIGN_FIELD (8) RuntimeObject* m_Items[1];

	inline RuntimeObject* GetAt(il2cpp_array_size_t index) const
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAt(il2cpp_array_size_t index)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		return m_Items + index;
	}
	inline void SetAt(il2cpp_array_size_t index, RuntimeObject* value)
	{
		IL2CPP_ARRAY_BOUNDS_CHECK(index, (uint32_t)(this)->max_length);
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
	inline RuntimeObject* GetAtUnchecked(il2cpp_array_size_t index) const
	{
		return m_Items[index];
	}
	inline RuntimeObject** GetAddressAtUnchecked(il2cpp_array_size_t index)
	{
		return m_Items + index;
	}
	inline void SetAtUnchecked(il2cpp_array_size_t index, RuntimeObject* value)
	{
		m_Items[index] = value;
		Il2CppCodeGenWriteBarrier((void**)m_Items + index, (void*)value);
	}
};


// T QFramework.AbstractAction`1<System.Object>::Allocate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* AbstractAction_1_Allocate_mE56E42C1F08802DE93D50F05AA244A59D48F0A28_gshared (const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Object>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Action_1__ctor_m2E1DFA67718FC1A0B6E5DFEB78831FFE9C059EB4_gshared (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
// System.Void QFramework.AbstractAction`1<System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AbstractAction_1__ctor_m74513D1DDF1D8ACFBFAB4D2923561B38555F620C_gshared (AbstractAction_1_t1F25168840FE7E9F00A1ACAB29227E65D6713A34* __this, const RuntimeMethod* method) ;
// T QFramework.BindableProperty`1<System.Boolean>::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_gshared (BindableProperty_1_t1ACEAE0BD8CBD20B51A6CACA4F4B56BE6F680D8A* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.Object,System.Int32>::ContainsKey(TKey)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_ContainsKey_m1087B74B4FF5004CBB6CC864FF1C87B6DB138505_gshared (Dictionary_2_t5C96F4B6841710A9013966F76224BAE01FB4B4D1* __this, RuntimeObject* ___0_key, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.Int32>::Add(TKey,TValue)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2_Add_m63897227AFA7035F1772315ABBBE7FD0A250E10C_gshared (Dictionary_2_t5C96F4B6841710A9013966F76224BAE01FB4B4D1* __this, RuntimeObject* ___0_key, int32_t ___1_value, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.Int32>::Clear()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2_Clear_m931E25EF2557C3A386E4B9DC8D8212B7D9D3F5AE_gshared (Dictionary_2_t5C96F4B6841710A9013966F76224BAE01FB4B4D1* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.Object,System.Int32>::TryGetValue(TKey,TValue&)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_TryGetValue_m4B8EE45640C70BBFD6F3EFF1040983404C098342_gshared (Dictionary_2_t5C96F4B6841710A9013966F76224BAE01FB4B4D1* __this, RuntimeObject* ___0_key, int32_t* ___1_value, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.Int32>::set_Item(TKey,TValue)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2_set_Item_m72CC2F1213D1C1B8ABEDE31082D07B67EC873B13_gshared (Dictionary_2_t5C96F4B6841710A9013966F76224BAE01FB4B4D1* __this, RuntimeObject* ___0_key, int32_t ___1_value, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.Object,System.Int32>::Remove(TKey)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_Remove_mFDB1C734B470EB31B094053D97F7749210922576_gshared (Dictionary_2_t5C96F4B6841710A9013966F76224BAE01FB4B4D1* __this, RuntimeObject* ___0_key, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.Int32>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2__ctor_m517E7F9D104FEAE6646EABDDC9C852510E86077C_gshared (Dictionary_2_t5C96F4B6841710A9013966F76224BAE01FB4B4D1* __this, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Object>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) ;
// QFramework.SafeObjectPool`1<T> QFramework.SafeObjectPool`1<System.Object>::get_Instance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR SafeObjectPool_1_t73BF818AB38FF94B7DEB8D5A485D353870E066B5* SafeObjectPool_1_get_Instance_m0E3A8F4A5E333A96387C7017D66AE9621CD92A46_gshared (const RuntimeMethod* method) ;
// System.Int32 System.Collections.Generic.Stack`1<System.Object>::get_Count()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Stack_1_get_Count_mD08AE71D49787D30DDD9D484BCD323D646744D2E_gshared_inline (Stack_1_tAD790A47551563636908E21E4F08C54C0C323EB5* __this, const RuntimeMethod* method) ;
// T System.Collections.Generic.Stack`1<System.Object>::Pop()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Stack_1_Pop_m2AFF69249659372F07EE25817DBCAFE74E1CF778_gshared (Stack_1_tAD790A47551563636908E21E4F08C54C0C323EB5* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Stack`1<System.Object>::Push(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Stack_1_Push_m709DD11BC1291A905814182CF9A367DE7399A778_gshared (Stack_1_tAD790A47551563636908E21E4F08C54C0C323EB5* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Stack`1<System.Object>::.ctor(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Stack_1__ctor_m25F8C6095172E75DEE8A43E857889659DFC4DCE9_gshared (Stack_1_tAD790A47551563636908E21E4F08C54C0C323EB5* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
// T UnityEngine.Resources::Load<System.Object>(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Resources_Load_TisRuntimeObject_mD1AF6299B14F87ED1D1A6199A51480919F7C79D7_gshared (String_t* ___0_path, const RuntimeMethod* method) ;
// UnityEngine.ResourceRequest UnityEngine.Resources::LoadAsync<System.Object>(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868* Resources_LoadAsync_TisRuntimeObject_m0BA0C560C99652A6C0A1B8BA2EBF6C7309527CE5_gshared (String_t* ___0_path, const RuntimeMethod* method) ;
// System.Void System.Action`2<System.Boolean,System.Object>::Invoke(T1,T2)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_2_Invoke_m5387D08742D6C89CAB31D981C0F63C08D70AB3AD_gshared_inline (Action_2_t4E94B0FCA1084D7868DB11A50767A4916CA3D3FB* __this, bool ___0_arg1, RuntimeObject* ___1_arg2, const RuntimeMethod* method) ;
// System.Void System.Func`1<System.Boolean>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Func_1__ctor_mDFFAE9C73346372438B5B04C4558AC42F1A3DA22_gshared (Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Boolean>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Action_1__ctor_mA8C3AC97D1F076EA5D1D0C10CEE6BD3E94711501_gshared (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
// System.Void QFramework.CustomProperty`1<System.Boolean>::.ctor(System.Func`1<T>,System.Action`1<T>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void CustomProperty_1__ctor_mF71DC5C64029750FFE0EB414A8D4AA9765719458_gshared (CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A* __this, Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457* ___0_valueGetter, Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* ___1_valueSetter, const RuntimeMethod* method) ;
// System.Void QFramework.BindableProperty`1<System.Boolean>::set_Value(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BindableProperty_1_set_Value_m4F9D20068AFC366BBC972B46D6A54C5FC8BDE651_gshared (BindableProperty_1_t1ACEAE0BD8CBD20B51A6CACA4F4B56BE6F680D8A* __this, bool ___0_value, const RuntimeMethod* method) ;
// System.Void QFramework.SafeObjectPool`1<System.Object>::Init(System.Int32,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void SafeObjectPool_1_Init_m5463C29F5264C55515E165A4013E4A7DA5076D2D_gshared (SafeObjectPool_1_t73BF818AB38FF94B7DEB8D5A485D353870E066B5* __this, int32_t ___0_maxCount, int32_t ___1_initCount, const RuntimeMethod* method) ;
// QFramework.IUnRegister QFramework.BindableProperty`1<System.Boolean>::Register(System.Action`1<T>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* BindableProperty_1_Register_mA9B66CC57EE9D6586607598395E59C8DA968FE87_gshared (BindableProperty_1_t1ACEAE0BD8CBD20B51A6CACA4F4B56BE6F680D8A* __this, Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* ___0_onValueChanged, const RuntimeMethod* method) ;
// System.Void System.Func`2<System.Collections.Generic.KeyValuePair`2<System.Object,System.Object>,System.Object>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Func_2__ctor_m60F64297108A01DFB5663C9BA121893957855907_gshared (Func_2_tF42287527472FA89789873F068A87C60A00EC7D3* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable::SelectMany<System.Collections.Generic.KeyValuePair`2<System.Object,System.Object>,System.Object>(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Collections.Generic.IEnumerable`1<TResult>>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Enumerable_SelectMany_TisKeyValuePair_2_tFC32D2507216293851350D29B64D79F950B55230_TisRuntimeObject_mBC4E65D1C67A31D677129ABC49C8DD701DD949F2_gshared (RuntimeObject* ___0_source, Func_2_t10FDFF9EE1C80F76FB5C31BA1D127F21CF49F9DA* ___1_selector, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.Object,System.Object>::ContainsKey(TKey)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Dictionary_2_ContainsKey_m703047C213F7AB55C9DC346596287773A1F670CD_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, RuntimeObject* ___0_key, const RuntimeMethod* method) ;
// TValue System.Collections.Generic.Dictionary`2<System.Object,System.Object>::get_Item(TKey)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Dictionary_2_get_Item_m4AAAECBE902A211BF2126E6AFA280AEF73A3E0D6_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, RuntimeObject* ___0_key, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Object>::Add(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.List`1<System.Object>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.Object>::Add(TKey,TValue)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2_Add_m93FFFABE8FCE7FA9793F0915E2A8842C7CD0C0C1_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, RuntimeObject* ___0_key, RuntimeObject* ___1_value, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.List`1<System.Object>::Remove(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Remove_m4DFA48F4CEB9169601E75FC28517C5C06EFA5AD7_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) ;
// T UnityEngine.Object::FindObjectOfType<System.Object>()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* Object_FindObjectOfType_TisRuntimeObject_m02DFBF011F3B59F777A5E521DB2A116DD496E968_gshared (const RuntimeMethod* method) ;
// T UnityEngine.GameObject::AddComponent<System.Object>()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* GameObject_AddComponent_TisRuntimeObject_m69B93700FACCF372F5753371C6E8FB780800B824_gshared (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method) ;
// T QFramework.MonoSingletonProperty`1<System.Object>::get_Instance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* MonoSingletonProperty_1_get_Instance_mD0C88CCD4E7E334A10A2DF17478341DD46279967_gshared (const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.Object>::Clear()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2_Clear_mCFB5EA7351D5860D2B91592B91A84CA265A41433_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.Object,System.Object>::.ctor(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Dictionary_2__ctor_mF225F49F6BE54C39563CECD7C693F0AE4F0530E8_gshared (Dictionary_2_t14FE4A752A83D53771C584E4C8D14E01F2AFD7BA* __this, int32_t ___0_capacity, const RuntimeMethod* method) ;
// TValue System.Collections.Generic.KeyValuePair`2<System.Object,System.Object>::get_Value()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* KeyValuePair_2_get_Value_mC6BD8075F9C9DDEF7B4D731E5C38EC19103988E7_gshared_inline (KeyValuePair_2_tFC32D2507216293851350D29B64D79F950B55230* __this, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Single>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Action_1__ctor_m770CD2F8BB65F2EDA5128CA2F96D71C35B23E859_gshared (Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
// QFramework.IUnRegister QFramework.BindableProperty`1<System.Single>::RegisterWithInitValue(System.Action`1<T>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* BindableProperty_1_RegisterWithInitValue_m50618DDD0FF1AADCBFF8D781BC9D6DE5AEA35824_gshared (BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* __this, Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* ___0_onValueChanged, const RuntimeMethod* method) ;
// System.Void System.Action`2<System.Boolean,System.Object>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Action_2__ctor_m910EDF0572CB2F28E45048A2EB215E25296E58BD_gshared (Action_2_t4E94B0FCA1084D7868DB11A50767A4916CA3D3FB* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Int32>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Action_1__ctor_m1BA854F3F4319EA4A4294DDFDA21C395B8D0FF87_gshared (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
// System.Void QFramework.BindableProperty`1<System.Single>::UnRegister(System.Action`1<T>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BindableProperty_1_UnRegister_mDE5848741098D28A95EE63902771C3214DB883CA_gshared (BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* __this, Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* ___0_onValueChanged, const RuntimeMethod* method) ;
// T QFramework.Property`1<System.Int32>::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Property_1_get_Value_mAB5065F7B5D8CB3B340114A5BB2953765536E0C7_gshared (Property_1_tEA2D0DDF24CE19765ADE39EFBB400F89850D7DA8* __this, const RuntimeMethod* method) ;
// System.Void QFramework.Property`1<System.Int32>::set_Value(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Property_1_set_Value_m314C5C5A72F5F0400ABFB00DFC1466A3434D26F3_gshared (Property_1_tEA2D0DDF24CE19765ADE39EFBB400F89850D7DA8* __this, int32_t ___0_value, const RuntimeMethod* method) ;
// System.Void QFramework.Property`1<System.Int32>::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Property_1__ctor_m77CDCAA18D9FC2F380162C19E268B4B1956628E8_gshared (Property_1_tEA2D0DDF24CE19765ADE39EFBB400F89850D7DA8* __this, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Int32>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mAC3C34BA1905AB5B79E483CD9BB082B7D667F703_gshared_inline (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* __this, int32_t ___0_obj, const RuntimeMethod* method) ;
// System.Void QFramework.BinaryHeap`1<System.Object>::Clear()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BinaryHeap_1_Clear_m6A0CBE3C113DCA81E2FBD07FA6C03905EB892BCD_gshared (BinaryHeap_1_tB7A2124313D61037F773E68E0596B02D8F126DF7* __this, const RuntimeMethod* method) ;
// System.Void QFramework.BinaryHeap`1<System.Object>::Insert(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BinaryHeap_1_Insert_m7ABB332CA3FA4532B0B919CD9F4D66240AD6C9AF_gshared (BinaryHeap_1_tB7A2124313D61037F773E68E0596B02D8F126DF7* __this, RuntimeObject* ___0_element, const RuntimeMethod* method) ;
// T QFramework.BinaryHeap`1<System.Object>::Pop()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* BinaryHeap_1_Pop_mEF7862B11FC719D83AB8DB8D29A422203D798DCE_gshared (BinaryHeap_1_tB7A2124313D61037F773E68E0596B02D8F126DF7* __this, const RuntimeMethod* method) ;
// T QFramework.BinaryHeap`1<System.Object>::Top()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* BinaryHeap_1_Top_m7EC63E2D65A9A75F608C02D09562C0BEF41FF986_gshared (BinaryHeap_1_tB7A2124313D61037F773E68E0596B02D8F126DF7* __this, const RuntimeMethod* method) ;
// System.Void QFramework.BinaryHeap`1<System.Object>::.ctor(System.Int32,QFramework.BinaryHeapSortMode)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void BinaryHeap_1__ctor_m7C1B84DA73F74FAE45B9D55CC334FE4A690C3864_gshared (BinaryHeap_1_tB7A2124313D61037F773E68E0596B02D8F126DF7* __this, int32_t ___0_minSize, int32_t ___1_sortMode, const RuntimeMethod* method) ;
// System.Void System.Predicate`1<System.Object>::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Predicate_1__ctor_m3E007299121A15DF80F4A210FF8C20E5DF688F20_gshared (Predicate_1_t8342C85FF4E41CD1F7024AC0CDC3E5312A32CB12* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.List`1<System.Object>::Exists(System.Predicate`1<T>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Exists_mE124D5A8B431C8B9B4C77EA23AD8B4C543829643_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, Predicate_1_t8342C85FF4E41CD1F7024AC0CDC3E5312A32CB12* ___0_match, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Single>::Invoke(T)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mA8F89FB04FEA0F48A4F22EC84B5F9ADB2914341F_gshared_inline (Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* __this, float ___0_obj, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.List`1<System.Object>::Contains(T)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool List_1_Contains_m4C9139C2A6B23E9343D3F87807B32C6E2CFE660D_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) ;
// T System.Collections.Generic.List`1<System.Object>::get_Item(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* List_1_get_Item_m33561245D64798C2AB07584C0EC4F240E4839A38_gshared (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, int32_t ___0_index, const RuntimeMethod* method) ;
// System.Int32 System.Collections.Generic.List`1<System.Object>::get_Count()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) ;

// T QFramework.AbstractAction`1<QFramework.PlaySoundAction>::Allocate()
inline PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* AbstractAction_1_Allocate_m87F117921135FDFEECA4721CC49E3FA41F235F6D (const RuntimeMethod* method)
{
	return ((  PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* (*) (const RuntimeMethod*))AbstractAction_1_Allocate_mE56E42C1F08802DE93D50F05AA244A59D48F0A28_gshared)(method);
}
// System.Void System.Action`1<QFramework.AudioPlayer>::.ctor(System.Object,System.IntPtr)
inline void Action_1__ctor_m3C560AF46A6955D28CF1A054DD88FD769247FDF0 (Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219*, RuntimeObject*, intptr_t, const RuntimeMethod*))Action_1__ctor_m2E1DFA67718FC1A0B6E5DFEB78831FFE9C059EB4_gshared)(__this, ___0_object, ___1_method, method);
}
// QFramework.AudioPlayer QFramework.AudioKit::PlaySound(System.String,System.Boolean,System.Action`1<QFramework.AudioPlayer>,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioKit_PlaySound_m9D36192E4F9A94A43432AE08EAB4852B703F6D29 (String_t* ___0_soundName, bool ___1_loop, Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* ___2_callBack, float ___3_volume, const RuntimeMethod* method) ;
// QFramework.AudioPlayer QFramework.AudioKit::PlaySound(UnityEngine.AudioClip,System.Boolean,System.Action`1<QFramework.AudioPlayer>,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioKit_PlaySound_m38FDA779D76D117AB6E58EA321E2DF0F1FA9B1E1 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, bool ___1_loop, Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* ___2_callBack, float ___3_volume, const RuntimeMethod* method) ;
// System.Void System.Action::Invoke()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AbstractAction`1<QFramework.PlaySoundAction>::.ctor()
inline void AbstractAction_1__ctor_m59DED3CAB019783282FD5DAC55A1646BF2E0A8BC (AbstractAction_1_t9A1EECE1BD7DD95509513E65E672298D45093CD3* __this, const RuntimeMethod* method)
{
	((  void (*) (AbstractAction_1_t9A1EECE1BD7DD95509513E65E672298D45093CD3*, const RuntimeMethod*))AbstractAction_1__ctor_m74513D1DDF1D8ACFBFAB4D2923561B38555F620C_gshared)(__this, method);
}
// System.Void QFramework.IActionExtensions::Finish(QFramework.IAction)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void IActionExtensions_Finish_m6D37A7CE87DA297AF4186858DBCFB0DA67C98570 (RuntimeObject* ___0_self, const RuntimeMethod* method) ;
// QFramework.PlaySoundAction QFramework.PlaySoundAction::Allocate(System.String,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* PlaySoundAction_Allocate_m7684EAD9B8D292D4E9A89B0B13118565247F5AFB (String_t* ___0_soundName, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___1_onFinish, const RuntimeMethod* method) ;
// QFramework.PlaySoundAction QFramework.PlaySoundAction::Allocate(UnityEngine.AudioClip,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* PlaySoundAction_Allocate_mE1701C1909915E2C3E474891ADBDA911BFFD8CEB (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_sound, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___1_onFinish, const RuntimeMethod* method) ;
// QFramework.AudioManager QFramework.AudioManager::get_Instance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA (const RuntimeMethod* method) ;
// QFramework.AudioPlayer QFramework.AudioManager::get_MusicPlayer()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioManager_get_MusicPlayer_m8046BC707549A3B916F8C074D12CB7E6C79E27BA_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioManager::CheckAudioListener()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_CheckAudioListener_m886498C2B5C2966BB1776BA22ED94F0732BF7FA4 (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioManager::set_CurrentMusicName(System.String)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioManager_set_CurrentMusicName_mE05C1458C0BCB217A49C8ABD589B4D4A8E77087C_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, String_t* ___0_value, const RuntimeMethod* method) ;
// System.Void UnityEngine.Debug::Log(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
// QFramework.AudioPlayer QFramework.AudioKit::get_MusicPlayer()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioKit_get_MusicPlayer_m570B8BF0A07102B221CC2A2AE9632854FBC31940 (const RuntimeMethod* method) ;
// QFramework.AudioPlayer QFramework.AudioPlayer::VolumeScale(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioPlayer_VolumeScale_mDD9640F3723126252B95FC8D6C3825E4E77FFC38 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, float ___0_volumeScale, const RuntimeMethod* method) ;
// QFramework.AudioPlayer QFramework.AudioPlayer::OnStart(System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioPlayer_OnStart_m6B06144CA7EE9C7117AE884B1EBBAEA727F6079D (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___0_onStart, const RuntimeMethod* method) ;
// UnityEngine.GameObject UnityEngine.Component::get_gameObject()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B (Component_t39FBE53E5EFCF4409111FB22C15FF73717632EC3* __this, const RuntimeMethod* method) ;
// QFramework.AudioPlayer QFramework.AudioPlayer::SetAudio(UnityEngine.GameObject,System.String,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioPlayer_SetAudio_mB62B93B946B1ED12976423AA09E6ED70827AB5ED (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___0_root, String_t* ___1_name, bool ___2_loop, const RuntimeMethod* method) ;
// System.Void QFramework.AudioKitOnFinishExtensions::OnFinish(QFramework.IAudioKitOnFinish,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitOnFinishExtensions_OnFinish_mC950466C398349874B4E4EC22F908C665E7A739A (RuntimeObject* ___0_self, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___1_onFinish, const RuntimeMethod* method) ;
// System.Void QFramework.AudioPlayer::Stop()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_Stop_m76865190573766D55E4533B8AEA5AE7F8834E96B (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioPlayer::Pause()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_Pause_mBFD7FA6BAA2AC4F968192F4F8A7FA8188EF865BC (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioPlayer::Resume()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_Resume_mC8F085A40D083FA2C0C2BE6D02492C58118F0522 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) ;
// QFramework.AudioPlayer QFramework.AudioManager::get_VoicePlayer()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioManager_get_VoicePlayer_m2FFBB17585A0BEEB38C67CF63A693A9F38C87D3D_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioManager::set_CurrentVoiceName(System.String)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioManager_set_CurrentVoiceName_m8203991273B51FA9A0FD8E9361C2D512E73CB6E4_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, String_t* ___0_value, const RuntimeMethod* method) ;
// QFramework.AudioKitSettings QFramework.AudioKit::get_Settings()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20_inline (const RuntimeMethod* method) ;
// QFramework.PlayerPrefsBooleanProperty QFramework.AudioKitSettings::get_IsVoiceOn()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* AudioKitSettings_get_IsVoiceOn_m40198BDFF0BAC22A00FF9A7D42A90D9AF6BDF937_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) ;
// T QFramework.BindableProperty`1<System.Boolean>::get_Value()
inline bool BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18 (BindableProperty_1_t1ACEAE0BD8CBD20B51A6CACA4F4B56BE6F680D8A* __this, const RuntimeMethod* method)
{
	return ((  bool (*) (BindableProperty_1_t1ACEAE0BD8CBD20B51A6CACA4F4B56BE6F680D8A*, const RuntimeMethod*))BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_gshared)(__this, method);
}
// QFramework.AudioPlayer QFramework.AudioKit::get_VoicePlayer()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioKit_get_VoicePlayer_mBA33429E58724BA68DC47981A3F322547F573AF9 (const RuntimeMethod* method) ;
// System.Int32 UnityEngine.Time::get_frameCount()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t Time_get_frameCount_m4A42E558A71301A216BDC49EC402D62F19C79667 (const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.String,System.Int32>::ContainsKey(TKey)
inline bool Dictionary_2_ContainsKey_mAEDD6BBEE1B37BC5E1D803803352FBE4CF4D3D7E (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* __this, String_t* ___0_key, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588*, String_t*, const RuntimeMethod*))Dictionary_2_ContainsKey_m1087B74B4FF5004CBB6CC864FF1C87B6DB138505_gshared)(__this, ___0_key, method);
}
// System.Void System.Collections.Generic.Dictionary`2<System.String,System.Int32>::Add(TKey,TValue)
inline void Dictionary_2_Add_m2FE98C9C3763E31D7CB55207ED3A46B33BF64883 (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* __this, String_t* ___0_key, int32_t ___1_value, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588*, String_t*, int32_t, const RuntimeMethod*))Dictionary_2_Add_m63897227AFA7035F1772315ABBBE7FD0A250E10C_gshared)(__this, ___0_key, ___1_value, method);
}
// System.Void System.Collections.Generic.Dictionary`2<System.String,System.Int32>::Clear()
inline void Dictionary_2_Clear_m841DBE29811833266CC127714688998A50D5F7CD (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* __this, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588*, const RuntimeMethod*))Dictionary_2_Clear_m931E25EF2557C3A386E4B9DC8D8212B7D9D3F5AE_gshared)(__this, method);
}
// System.Boolean System.Collections.Generic.Dictionary`2<System.String,System.Int32>::TryGetValue(TKey,TValue&)
inline bool Dictionary_2_TryGetValue_m835BB1E6EA8A8BF1242B51E28FD65B43FEF68E2A (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* __this, String_t* ___0_key, int32_t* ___1_value, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588*, String_t*, int32_t*, const RuntimeMethod*))Dictionary_2_TryGetValue_m4B8EE45640C70BBFD6F3EFF1040983404C098342_gshared)(__this, ___0_key, ___1_value, method);
}
// System.Void System.Collections.Generic.Dictionary`2<System.String,System.Int32>::set_Item(TKey,TValue)
inline void Dictionary_2_set_Item_m038480C0EC13713DBD89A53BE69FF0359501B4C2 (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* __this, String_t* ___0_key, int32_t ___1_value, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588*, String_t*, int32_t, const RuntimeMethod*))Dictionary_2_set_Item_m72CC2F1213D1C1B8ABEDE31082D07B67EC873B13_gshared)(__this, ___0_key, ___1_value, method);
}
// System.Boolean System.Collections.Generic.Dictionary`2<System.String,System.Int32>::Remove(TKey)
inline bool Dictionary_2_Remove_m0C90DB78F4134CDEB001F338AA2745F8D9651CAC (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* __this, String_t* ___0_key, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588*, String_t*, const RuntimeMethod*))Dictionary_2_Remove_mFDB1C734B470EB31B094053D97F7749210922576_gshared)(__this, ___0_key, method);
}
// System.Void QFramework.AudioKit/<>c__DisplayClass21_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass21_0__ctor_m842ED89D76A35F5192C21F17A9D41BFF9CB7FB30 (U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* __this, const RuntimeMethod* method) ;
// QFramework.PlayerPrefsBooleanProperty QFramework.AudioKitSettings::get_IsSoundOn()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* AudioKitSettings_get_IsSoundOn_m16E5D82CD5CE9BDAA868820A75FA970239276A85_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) ;
// System.Boolean QFramework.AudioKit::CanPlaySound(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioKit_CanPlaySound_m42B1801F93A991904D3EEF8C77AE2A56F01CA5A1 (String_t* ___0_soundName, const RuntimeMethod* method) ;
// QFramework.PlayerPrefsFloatProperty QFramework.AudioKitSettings::get_SoundVolume()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* AudioKitSettings_get_SoundVolume_mD628B2C76B4EE61E1BF923E41126A63B0F3B897A_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) ;
// QFramework.AudioPlayer QFramework.AudioPlayer::Allocate(QFramework.BindableProperty`1<System.Single>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioPlayer_Allocate_m7E4B1CB936A2FB00F782DA45373DF120B5C35646 (BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* ___0_volume, const RuntimeMethod* method) ;
// System.Void System.Action::.ctor(System.Object,System.IntPtr)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method) ;
// System.Void QFramework.AudioManager::AddSoundPlayer2Pool(QFramework.AudioPlayer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_AddSoundPlayer2Pool_m65FCF7B9091A32D9C58119B23787DDFEBB820B2E (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_audioPlayer, const RuntimeMethod* method) ;
// System.Void QFramework.AudioManager::ForEachAllSound(System.Action`1<QFramework.AudioPlayer>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_ForEachAllSound_m937C7CCCD694ECFFC9A32A0F90389A389B046EB2 (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* ___0_operation, const RuntimeMethod* method) ;
// System.Void QFramework.AudioManager::ClearAllPlayingSound()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_ClearAllPlayingSound_mF5A45B5E7FC9FA2AAB1ECA1A5CEA41F812304278 (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) ;
// System.String System.Int32::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Int32_ToString_m030E01C24E294D6762FB0B6F37CB541581F55CA5 (int32_t* __this, const RuntimeMethod* method) ;
// System.String System.String::Concat(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m9E3155FB84015C823606188F53B47CB44C444991 (String_t* ___0_str0, String_t* ___1_str1, const RuntimeMethod* method) ;
// System.String QFramework.AudioManager::get_CurrentMusicName()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* AudioManager_get_CurrentMusicName_m46E7270EA32A4B0CD9B4261AF7CB78A75C6444FB_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioPlayer::SetAudioExt(UnityEngine.GameObject,UnityEngine.AudioClip,System.String,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_SetAudioExt_m159E924BA175BD34E5A784D5B274816B100994C7 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___0_root, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, String_t* ___2_name, bool ___3_loop, const RuntimeMethod* method) ;
// System.String QFramework.AudioManager::get_CurrentVoiceName()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* AudioManager_get_CurrentVoiceName_m2A6026179BDD9F9E07A287B6935786E0CCF24FA4_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioKit/<>c__DisplayClass25_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass25_0__ctor_m74B5819BBCF04D3324DF32EAABD3064AAC6A16C2 (U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51* __this, const RuntimeMethod* method) ;
// System.String UnityEngine.Object::get_name()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* Object_get_name_mAC2F6B897CF1303BA4249B4CB55271AFACBB6392 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* __this, const RuntimeMethod* method) ;
// QFramework.FluentMusicAPI QFramework.FluentMusicAPI::Allocate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* FluentMusicAPI_Allocate_m47EA60F843D95341E109C9D8FDDC8436B003579E (const RuntimeMethod* method) ;
// QFramework.FluentSoundAPI QFramework.FluentSoundAPI::Allocate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* FluentSoundAPI_Allocate_mCB696368A37809648CBB30613D38F8CE94DF18A0 (const RuntimeMethod* method) ;
// System.Void System.Object::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2 (RuntimeObject* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioKitConfig::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitConfig__ctor_m4209BF93AC62060B70335750DE6D083C7699565E (AudioKitConfig_t0518724CE4F14A672627665BE74D533751686A54* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.String,System.Int32>::.ctor()
inline void Dictionary_2__ctor_mA3C3860EDE2CDD08BBD68C389377BC89D029D968 (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* __this, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588*, const RuntimeMethod*))Dictionary_2__ctor_m517E7F9D104FEAE6646EABDDC9C852510E86077C_gshared)(__this, method);
}
// System.Void QFramework.AudioKitSettings::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitSettings__ctor_m0D57B7E51C1244C01164A711F52CA0FB77DD88F1 (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) ;
// System.Void System.Action`1<QFramework.AudioPlayer>::Invoke(T)
inline void Action_1_Invoke_m0983EE053D0C9620014033A591393EE7C83C9A97_inline (Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_obj, const RuntimeMethod* method)
{
	((  void (*) (Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219*, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59*, const RuntimeMethod*))Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline)(__this, ___0_obj, method);
}
// System.Void QFramework.AudioManager::RemoveSoundPlayerFromPool(QFramework.AudioPlayer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_RemoveSoundPlayerFromPool_m144B7764E7D26C9EF81AE0F0C8E9BBAB74297E0E (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_audioPlayer, const RuntimeMethod* method) ;
// System.Void QFramework.AudioKit::SoundFinish(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_SoundFinish_m5A8751B115239ABA1523914682619A52B948FD2D (String_t* ___0_soundName, const RuntimeMethod* method) ;
// System.Void QFramework.AudioKit/<>c::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__ctor_mEA1449FC60866C3F2C7CBA531E071E4E49F48865 (U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC* __this, const RuntimeMethod* method) ;
// System.Void QFramework.DefaultAudioLoaderPool::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DefaultAudioLoaderPool__ctor_m23B68C1F6795C1125F1229442C9E30486CC27751 (DefaultAudioLoaderPool_t83CA69E29CA127237BADCBB67697143A0ED7D53A* __this, const RuntimeMethod* method) ;
// System.String System.String::Concat(System.String,System.String,System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* String_Concat_m093934F71A9B351911EE46311674ED463B180006 (String_t* ___0_str0, String_t* ___1_str1, String_t* ___2_str2, String_t* ___3_str3, const RuntimeMethod* method) ;
// QFramework.SafeObjectPool`1<T> QFramework.SafeObjectPool`1<QFramework.AudioSearchKeys>::get_Instance()
inline SafeObjectPool_1_t7D55BDF522ED56ECFF3CBDB6115923D35053E535* SafeObjectPool_1_get_Instance_m10308F90A4BD9A0D5A63EC07A735B36D00C075ED (const RuntimeMethod* method)
{
	return ((  SafeObjectPool_1_t7D55BDF522ED56ECFF3CBDB6115923D35053E535* (*) (const RuntimeMethod*))SafeObjectPool_1_get_Instance_m0E3A8F4A5E333A96387C7017D66AE9621CD92A46_gshared)(method);
}
// System.Int32 System.Collections.Generic.Stack`1<QFramework.IAudioLoader>::get_Count()
inline int32_t Stack_1_get_Count_mE28FA86A5FE81403FA112BE1E3D928DAFCD15F0E_inline (Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5*, const RuntimeMethod*))Stack_1_get_Count_mD08AE71D49787D30DDD9D484BCD323D646744D2E_gshared_inline)(__this, method);
}
// T System.Collections.Generic.Stack`1<QFramework.IAudioLoader>::Pop()
inline RuntimeObject* Stack_1_Pop_m7109081E7676FB662FEB90062F1A6C4A288E596A (Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5* __this, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5*, const RuntimeMethod*))Stack_1_Pop_m2AFF69249659372F07EE25817DBCAFE74E1CF778_gshared)(__this, method);
}
// System.Void System.Collections.Generic.Stack`1<QFramework.IAudioLoader>::Push(T)
inline void Stack_1_Push_m837A07DFF4ED3EC7B750D3EF3E9DFF8E4C713DD0 (Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5* __this, RuntimeObject* ___0_item, const RuntimeMethod* method)
{
	((  void (*) (Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5*, RuntimeObject*, const RuntimeMethod*))Stack_1_Push_m709DD11BC1291A905814182CF9A367DE7399A778_gshared)(__this, ___0_item, method);
}
// System.Void System.Collections.Generic.Stack`1<QFramework.IAudioLoader>::.ctor(System.Int32)
inline void Stack_1__ctor_mDA3004D09A08CCD4F349E97A1A95B76B9D5D41F6 (Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5* __this, int32_t ___0_capacity, const RuntimeMethod* method)
{
	((  void (*) (Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5*, int32_t, const RuntimeMethod*))Stack_1__ctor_m25F8C6095172E75DEE8A43E857889659DFC4DCE9_gshared)(__this, ___0_capacity, method);
}
// System.Void QFramework.DefaultAudioLoader::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DefaultAudioLoader__ctor_mEE4834113F8F8C71ECE19AC27F895E2F0C85B95F (DefaultAudioLoader_tA58E6CBC81292CF195A2FED8AF13ABF1473CF43F* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AbstractAudioLoaderPool::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AbstractAudioLoaderPool__ctor_m7A9A7C0BE215C31A8C21932F912A67EE678D7C02 (AbstractAudioLoaderPool_tA9514ECF9B0920914A41DA1E94F9C7914109136A* __this, const RuntimeMethod* method) ;
// T UnityEngine.Resources::Load<UnityEngine.AudioClip>(System.String)
inline AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* Resources_Load_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m8D55846FD24C1D133D4EA744BE1E73E054B93A78 (String_t* ___0_path, const RuntimeMethod* method)
{
	return ((  AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* (*) (String_t*, const RuntimeMethod*))Resources_Load_TisRuntimeObject_mD1AF6299B14F87ED1D1A6199A51480919F7C79D7_gshared)(___0_path, method);
}
// System.Void QFramework.DefaultAudioLoader/<>c__DisplayClass4_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass4_0__ctor_mD7A7A1A6DB4B58B568D36E7B3BE49C1A6ED05908 (U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B* __this, const RuntimeMethod* method) ;
// UnityEngine.ResourceRequest UnityEngine.Resources::LoadAsync<UnityEngine.AudioClip>(System.String)
inline ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868* Resources_LoadAsync_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m0D27E9CB67015D736F22E3AFAB5E6108ABA9A740 (String_t* ___0_path, const RuntimeMethod* method)
{
	return ((  ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868* (*) (String_t*, const RuntimeMethod*))Resources_LoadAsync_TisRuntimeObject_m0BA0C560C99652A6C0A1B8BA2EBF6C7309527CE5_gshared)(___0_path, method);
}
// System.Void System.Action`1<UnityEngine.AsyncOperation>::.ctor(System.Object,System.IntPtr)
inline void Action_1__ctor_m33ABB7530487276910BEFB499A97D33FB2E06D7D (Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB*, RuntimeObject*, intptr_t, const RuntimeMethod*))Action_1__ctor_m2E1DFA67718FC1A0B6E5DFEB78831FFE9C059EB4_gshared)(__this, ___0_object, ___1_method, method);
}
// System.Void UnityEngine.AsyncOperation::add_completed(System.Action`1<UnityEngine.AsyncOperation>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AsyncOperation_add_completed_mD6F21BA8127D6D4B7ABDEFAA995A7A347A20A793 (AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C* __this, Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB* ___0_value, const RuntimeMethod* method) ;
// System.Void UnityEngine.Resources::UnloadAsset(UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Resources_UnloadAsset_m4828D1393356C052061C66403B1437F7D7E21908 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_assetToUnload, const RuntimeMethod* method) ;
// UnityEngine.Object UnityEngine.ResourceRequest::get_asset()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ResourceRequest_get_asset_m740C394902843D080795A49372A2AB3CE9705087 (ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868* __this, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Object::op_Implicit(UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Implicit_m93896EF7D68FA113C42D3FE2BC6F661FC7EF514A (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_exists, const RuntimeMethod* method) ;
// System.Void System.Action`2<System.Boolean,UnityEngine.AudioClip>::Invoke(T1,T2)
inline void Action_2_Invoke_mF9B25C8213BEBD9D83124082D0CDB0859A9F016D_inline (Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD* __this, bool ___0_arg1, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_arg2, const RuntimeMethod* method)
{
	((  void (*) (Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD*, bool, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*, const RuntimeMethod*))Action_2_Invoke_m5387D08742D6C89CAB31D981C0F63C08D70AB3AD_gshared_inline)(__this, ___0_arg1, ___1_arg2, method);
}
// System.Void QFramework.PlayerPrefsBooleanProperty::.ctor(System.String,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlayerPrefsBooleanProperty__ctor_m4109A6099AC15D8224C30BD7FE6F800967C2D541 (PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* __this, String_t* ___0_saveKey, bool ___1_defaultValue, const RuntimeMethod* method) ;
// System.Void QFramework.AudioKitSettings::set_IsSoundOn(QFramework.PlayerPrefsBooleanProperty)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_IsSoundOn_m3B8938CB24A93E9864A4152064F6A7E66B12FD22_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* ___0_value, const RuntimeMethod* method) ;
// System.Void QFramework.AudioKitSettings::set_IsMusicOn(QFramework.PlayerPrefsBooleanProperty)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_IsMusicOn_mB7BB81A6F1F2CF2A58C28A0FF40783B6603D48CB_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* ___0_value, const RuntimeMethod* method) ;
// System.Void QFramework.AudioKitSettings::set_IsVoiceOn(QFramework.PlayerPrefsBooleanProperty)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_IsVoiceOn_m22A05BA843579B70656A6B4B7C95163882DD30C0_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* ___0_value, const RuntimeMethod* method) ;
// System.Void System.Func`1<System.Boolean>::.ctor(System.Object,System.IntPtr)
inline void Func_1__ctor_mDFFAE9C73346372438B5B04C4558AC42F1A3DA22 (Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457*, RuntimeObject*, intptr_t, const RuntimeMethod*))Func_1__ctor_mDFFAE9C73346372438B5B04C4558AC42F1A3DA22_gshared)(__this, ___0_object, ___1_method, method);
}
// System.Void System.Action`1<System.Boolean>::.ctor(System.Object,System.IntPtr)
inline void Action_1__ctor_mA8C3AC97D1F076EA5D1D0C10CEE6BD3E94711501 (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C*, RuntimeObject*, intptr_t, const RuntimeMethod*))Action_1__ctor_mA8C3AC97D1F076EA5D1D0C10CEE6BD3E94711501_gshared)(__this, ___0_object, ___1_method, method);
}
// System.Void QFramework.CustomProperty`1<System.Boolean>::.ctor(System.Func`1<T>,System.Action`1<T>)
inline void CustomProperty_1__ctor_mF71DC5C64029750FFE0EB414A8D4AA9765719458 (CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A* __this, Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457* ___0_valueGetter, Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* ___1_valueSetter, const RuntimeMethod* method)
{
	((  void (*) (CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A*, Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457*, Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C*, const RuntimeMethod*))CustomProperty_1__ctor_mF71DC5C64029750FFE0EB414A8D4AA9765719458_gshared)(__this, ___0_valueGetter, ___1_valueSetter, method);
}
// System.Void QFramework.AudioKitSettings::set_IsOn(QFramework.CustomProperty`1<System.Boolean>)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_IsOn_m4E88FB457E5A9FA95CAB7591BD86C9ECC5A7742E_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A* ___0_value, const RuntimeMethod* method) ;
// System.Void QFramework.PlayerPrefsFloatProperty::.ctor(System.String,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlayerPrefsFloatProperty__ctor_mC05DD0E1587A27B45A0CFB2E97B3E288A38CD7E0 (PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* __this, String_t* ___0_saveKey, float ___1_defaultValue, const RuntimeMethod* method) ;
// System.Void QFramework.AudioKitSettings::set_SoundVolume(QFramework.PlayerPrefsFloatProperty)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_SoundVolume_mAB196708DD249054162A3B89B53CD1C5564A9954_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* ___0_value, const RuntimeMethod* method) ;
// System.Void QFramework.AudioKitSettings::set_MusicVolume(QFramework.PlayerPrefsFloatProperty)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_MusicVolume_m31F575D40B044BC109FE19B8AEF8575C8995A55B_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* ___0_value, const RuntimeMethod* method) ;
// System.Void QFramework.AudioKitSettings::set_VoiceVolume(QFramework.PlayerPrefsFloatProperty)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_VoiceVolume_m9477C782964C3652242B54F32C82906A44350D3A_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* ___0_value, const RuntimeMethod* method) ;
// QFramework.PlayerPrefsBooleanProperty QFramework.AudioKitSettings::get_IsMusicOn()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* AudioKitSettings_get_IsMusicOn_mB0034F4E51AEAC7FF364B848A6A42CD30E1CBB89_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) ;
// System.Void QFramework.BindableProperty`1<System.Boolean>::set_Value(T)
inline void BindableProperty_1_set_Value_m4F9D20068AFC366BBC972B46D6A54C5FC8BDE651 (BindableProperty_1_t1ACEAE0BD8CBD20B51A6CACA4F4B56BE6F680D8A* __this, bool ___0_value, const RuntimeMethod* method)
{
	((  void (*) (BindableProperty_1_t1ACEAE0BD8CBD20B51A6CACA4F4B56BE6F680D8A*, bool, const RuntimeMethod*))BindableProperty_1_set_Value_m4F9D20068AFC366BBC972B46D6A54C5FC8BDE651_gshared)(__this, ___0_value, method);
}
// QFramework.SafeObjectPool`1<T> QFramework.SafeObjectPool`1<QFramework.AudioPlayer>::get_Instance()
inline SafeObjectPool_1_t3542EC4AA4871EFF26264DA85DCDB6C39D22A8AD* SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421 (const RuntimeMethod* method)
{
	return ((  SafeObjectPool_1_t3542EC4AA4871EFF26264DA85DCDB6C39D22A8AD* (*) (const RuntimeMethod*))SafeObjectPool_1_get_Instance_m0E3A8F4A5E333A96387C7017D66AE9621CD92A46_gshared)(method);
}
// System.Void QFramework.SafeObjectPool`1<QFramework.AudioPlayer>::Init(System.Int32,System.Int32)
inline void SafeObjectPool_1_Init_mB381C8FD59EE0D59D0AB6A93C6CE4DBEA1C1CA43 (SafeObjectPool_1_t3542EC4AA4871EFF26264DA85DCDB6C39D22A8AD* __this, int32_t ___0_maxCount, int32_t ___1_initCount, const RuntimeMethod* method)
{
	((  void (*) (SafeObjectPool_1_t3542EC4AA4871EFF26264DA85DCDB6C39D22A8AD*, int32_t, int32_t, const RuntimeMethod*))SafeObjectPool_1_Init_m5463C29F5264C55515E165A4013E4A7DA5076D2D_gshared)(__this, ___0_maxCount, ___1_initCount, method);
}
// QFramework.PlayerPrefsFloatProperty QFramework.AudioKitSettings::get_MusicVolume()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* AudioKitSettings_get_MusicVolume_mC12F412255E5C5D80823C6B401EA3B76CEA9EC84_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioManager::set_MusicPlayer(QFramework.AudioPlayer)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioManager_set_MusicPlayer_m19CF9C6DCBACE60B3B532458F590BE8B4A6B9ACF_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_value, const RuntimeMethod* method) ;
// System.Void QFramework.AudioPlayer::set_UsedCache(System.Boolean)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioPlayer_set_UsedCache_m0D255C3A9BF12259B211DFCF4C155AE5EC2A2DEA_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, bool ___0_value, const RuntimeMethod* method) ;
// System.Void QFramework.AudioPlayer::set_IsLoop(System.Boolean)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioPlayer_set_IsLoop_mB1AC04B836A3D80C92450B55197BDDFF6463AC17_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, bool ___0_value, const RuntimeMethod* method) ;
// QFramework.PlayerPrefsFloatProperty QFramework.AudioKitSettings::get_VoiceVolume()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* AudioKitSettings_get_VoiceVolume_m67A62365F356F9A35A31A385452EDBCBFB99F3C1_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioManager::set_VoicePlayer(QFramework.AudioPlayer)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioManager_set_VoicePlayer_mE9427043D0CD26480779BA21020D4C22FD7E40BC_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_value, const RuntimeMethod* method) ;
// UnityEngine.Transform UnityEngine.GameObject::get_transform()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* GameObject_get_transform_m0BC10ADFA1632166AE5544BDF9038A2650C2AE56 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method) ;
// UnityEngine.Vector3 UnityEngine.Vector3::get_zero()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_zero_m0C1249C3F25B1C70EAD3CC8B31259975A457AE39_inline (const RuntimeMethod* method) ;
// System.Void UnityEngine.Transform::set_position(UnityEngine.Vector3)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156 (Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* __this, Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 ___0_value, const RuntimeMethod* method) ;
// QFramework.IUnRegister QFramework.BindableProperty`1<System.Boolean>::Register(System.Action`1<T>)
inline RuntimeObject* BindableProperty_1_Register_mA9B66CC57EE9D6586607598395E59C8DA968FE87 (BindableProperty_1_t1ACEAE0BD8CBD20B51A6CACA4F4B56BE6F680D8A* __this, Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* ___0_onValueChanged, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (BindableProperty_1_t1ACEAE0BD8CBD20B51A6CACA4F4B56BE6F680D8A*, Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C*, const RuntimeMethod*))BindableProperty_1_Register_mA9B66CC57EE9D6586607598395E59C8DA968FE87_gshared)(__this, ___0_onValueChanged, method);
}
// QFramework.IUnRegister QFramework.UnRegisterExtension::UnRegisterWhenGameObjectDestroyed(QFramework.IUnRegister,UnityEngine.GameObject)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* UnRegisterExtension_UnRegisterWhenGameObjectDestroyed_m8D6632ECF8FF05C690C60F2AC00C492A6B7BEBB7 (RuntimeObject* ___0_unRegister, GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___1_gameObject, const RuntimeMethod* method) ;
// System.Void System.Func`2<System.Collections.Generic.KeyValuePair`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>,System.Collections.Generic.IEnumerable`1<QFramework.AudioPlayer>>::.ctor(System.Object,System.IntPtr)
inline void Func_2__ctor_mB667DF572ECCB07C1425B90525AFA36B56845C1C (Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB*, RuntimeObject*, intptr_t, const RuntimeMethod*))Func_2__ctor_m60F64297108A01DFB5663C9BA121893957855907_gshared)(__this, ___0_object, ___1_method, method);
}
// System.Collections.Generic.IEnumerable`1<TResult> System.Linq.Enumerable::SelectMany<System.Collections.Generic.KeyValuePair`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>,QFramework.AudioPlayer>(System.Collections.Generic.IEnumerable`1<TSource>,System.Func`2<TSource,System.Collections.Generic.IEnumerable`1<TResult>>)
inline RuntimeObject* Enumerable_SelectMany_TisKeyValuePair_2_tA1ACD899BEC543AE9704AB31AC4C62160B215F43_TisAudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59_mAC2CE1EF65E76E2298ACF9EC5852CA617133628D (RuntimeObject* ___0_source, Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB* ___1_selector, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (RuntimeObject*, Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB*, const RuntimeMethod*))Enumerable_SelectMany_TisKeyValuePair_2_tFC32D2507216293851350D29B64D79F950B55230_TisRuntimeObject_mBC4E65D1C67A31D677129ABC49C8DD701DD949F2_gshared)(___0_source, ___1_selector, method);
}
// System.String QFramework.AudioPlayer::get_GetName()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* AudioPlayer_get_GetName_mCD8D1164E27DDF8F85DD7DF840EF0E4C77340060_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>::ContainsKey(TKey)
inline bool Dictionary_2_ContainsKey_m6FE0713BCEFD16DC00E8636D5B8DD3BA313536E4 (Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* __this, String_t* ___0_key, const RuntimeMethod* method)
{
	return ((  bool (*) (Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032*, String_t*, const RuntimeMethod*))Dictionary_2_ContainsKey_m703047C213F7AB55C9DC346596287773A1F670CD_gshared)(__this, ___0_key, method);
}
// TValue System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>::get_Item(TKey)
inline List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* Dictionary_2_get_Item_mF0187134582D2A345C1E9A834E336B5A5F96DEC9 (Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* __this, String_t* ___0_key, const RuntimeMethod* method)
{
	return ((  List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* (*) (Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032*, String_t*, const RuntimeMethod*))Dictionary_2_get_Item_m4AAAECBE902A211BF2126E6AFA280AEF73A3E0D6_gshared)(__this, ___0_key, method);
}
// System.Void System.Collections.Generic.List`1<QFramework.AudioPlayer>::Add(T)
inline void List_1_Add_m6320686C1369BBBA0D20DBC782155A5B2A00BAC5_inline (List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_item, const RuntimeMethod* method)
{
	((  void (*) (List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D*, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59*, const RuntimeMethod*))List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline)(__this, ___0_item, method);
}
// System.Void System.Collections.Generic.List`1<QFramework.AudioPlayer>::.ctor()
inline void List_1__ctor_mEB86DAF271C6934999B430DFFDA0C2B4BE67E398 (List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// System.Void System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>::Add(TKey,TValue)
inline void Dictionary_2_Add_mAC3ECB943BFF17C600E7EC191AB4A4E4C6E885F4 (Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* __this, String_t* ___0_key, List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* ___1_value, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032*, String_t*, List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D*, const RuntimeMethod*))Dictionary_2_Add_m93FFFABE8FCE7FA9793F0915E2A8842C7CD0C0C1_gshared)(__this, ___0_key, ___1_value, method);
}
// System.Boolean System.Collections.Generic.List`1<QFramework.AudioPlayer>::Remove(T)
inline bool List_1_Remove_m32474DB046D95F7ABA9D1985B34A186E4F41CDE0 (List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_item, const RuntimeMethod* method)
{
	return ((  bool (*) (List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D*, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59*, const RuntimeMethod*))List_1_Remove_m4DFA48F4CEB9169601E75FC28517C5C06EFA5AD7_gshared)(__this, ___0_item, method);
}
// T UnityEngine.Object::FindObjectOfType<UnityEngine.AudioListener>()
inline AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35* Object_FindObjectOfType_TisAudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35_mFDA0D604F7239642B39B6010674A936ADD544912 (const RuntimeMethod* method)
{
	return ((  AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35* (*) (const RuntimeMethod*))Object_FindObjectOfType_TisRuntimeObject_m02DFBF011F3B59F777A5E521DB2A116DD496E968_gshared)(method);
}
// T UnityEngine.GameObject::AddComponent<UnityEngine.AudioListener>()
inline AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35* GameObject_AddComponent_TisAudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35_mA30AC51FE6287A9D0057077D99F694600A3D844E (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method)
{
	return ((  AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35* (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, const RuntimeMethod*))GameObject_AddComponent_TisRuntimeObject_m69B93700FACCF372F5753371C6E8FB780800B824_gshared)(__this, method);
}
// System.Boolean System.String::IsNullOrEmpty(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478 (String_t* ___0_value, const RuntimeMethod* method) ;
// T QFramework.MonoSingletonProperty`1<QFramework.AudioManager>::get_Instance()
inline AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* MonoSingletonProperty_1_get_Instance_m2BF4163558BC28C3B9119589FDD486ADC669CA7F (const RuntimeMethod* method)
{
	return ((  AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* (*) (const RuntimeMethod*))MonoSingletonProperty_1_get_Instance_mD0C88CCD4E7E334A10A2DF17478341DD46279967_gshared)(method);
}
// System.Void System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>::Clear()
inline void Dictionary_2_Clear_mAAB064453A17DFB0A15C203ECF907FFA7AF7D46E (Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* __this, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032*, const RuntimeMethod*))Dictionary_2_Clear_mCFB5EA7351D5860D2B91592B91A84CA265A41433_gshared)(__this, method);
}
// System.Void UnityEngine.MonoBehaviour::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E (MonoBehaviour_t532A11E69716D348D8AA7F854AFCBFCB8AD17F71* __this, const RuntimeMethod* method) ;
// System.Void System.Collections.Generic.Dictionary`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>::.ctor(System.Int32)
inline void Dictionary_2__ctor_mFE946D85F78DE391E71EA905A87F0BE5F34A041F (Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* __this, int32_t ___0_capacity, const RuntimeMethod* method)
{
	((  void (*) (Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032*, int32_t, const RuntimeMethod*))Dictionary_2__ctor_mF225F49F6BE54C39563CECD7C693F0AE4F0530E8_gshared)(__this, ___0_capacity, method);
}
// System.Void QFramework.AudioKit::PlayMusic(System.String,System.Boolean,System.Action,System.Action,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_PlayMusic_m024F70A37392A7DB77FD290AAA197DCF0FA7B8F6 (String_t* ___0_musicName, bool ___1_loop, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___2_onBeganCallback, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___3_onEndCallback, float ___4_volume, const RuntimeMethod* method) ;
// System.Void QFramework.AudioKit::PlayVoice(System.String,System.Boolean,System.Action,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_PlayVoice_m0DB27FC1DA555F0F196CD00346055B002B7DAC92 (String_t* ___0_voiceName, bool ___1_loop, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___2_onBeganCallback, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___3_onEndedCallback, const RuntimeMethod* method) ;
// System.Void QFramework.AudioManager/<>c::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__ctor_m0823286159044D6323AB819D03043C253523655C (U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86* __this, const RuntimeMethod* method) ;
// TValue System.Collections.Generic.KeyValuePair`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>::get_Value()
inline List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* KeyValuePair_2_get_Value_m267D88C38008E34C628B6E9037CB9FB98C40C334_inline (KeyValuePair_2_tA1ACD899BEC543AE9704AB31AC4C62160B215F43* __this, const RuntimeMethod* method)
{
	return ((  List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* (*) (KeyValuePair_2_tA1ACD899BEC543AE9704AB31AC4C62160B215F43*, const RuntimeMethod*))KeyValuePair_2_get_Value_mC6BD8075F9C9DDEF7B4D731E5C38EC19103988E7_gshared_inline)(__this, method);
}
// System.Void QFramework.AudioPlayer::set_Volume(QFramework.BindableProperty`1<System.Single>)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioPlayer_set_Volume_m8D35738EFB189D11C6FBFA3A1C116B3C4D007F5E_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* ___0_value, const RuntimeMethod* method) ;
// System.Void QFramework.AudioPlayer::OnAllocate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_OnAllocate_m9B7CA46E03DC8460D4BC05D1162E5EC0D4989C33 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) ;
// QFramework.BindableProperty`1<System.Single> QFramework.AudioPlayer::get_Volume()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* AudioPlayer_get_Volume_m2095A2461F76393AA590905BEB51E7E90A174126_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Single>::.ctor(System.Object,System.IntPtr)
inline void Action_1__ctor_m770CD2F8BB65F2EDA5128CA2F96D71C35B23E859 (Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A*, RuntimeObject*, intptr_t, const RuntimeMethod*))Action_1__ctor_m770CD2F8BB65F2EDA5128CA2F96D71C35B23E859_gshared)(__this, ___0_object, ___1_method, method);
}
// QFramework.IUnRegister QFramework.BindableProperty`1<System.Single>::RegisterWithInitValue(System.Action`1<T>)
inline RuntimeObject* BindableProperty_1_RegisterWithInitValue_m50618DDD0FF1AADCBFF8D781BC9D6DE5AEA35824 (BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* __this, Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* ___0_onValueChanged, const RuntimeMethod* method)
{
	return ((  RuntimeObject* (*) (BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58*, Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A*, const RuntimeMethod*))BindableProperty_1_RegisterWithInitValue_m50618DDD0FF1AADCBFF8D781BC9D6DE5AEA35824_gshared)(__this, ___0_onValueChanged, method);
}
// System.Delegate System.Delegate::Combine(System.Delegate,System.Delegate)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Delegate_t* Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00 (Delegate_t* ___0_a, Delegate_t* ___1_b, const RuntimeMethod* method) ;
// System.Boolean UnityEngine.Object::op_Equality(UnityEngine.Object,UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
// System.Boolean System.String::op_Equality(System.String,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1 (String_t* ___0_a, String_t* ___1_b, const RuntimeMethod* method) ;
// T UnityEngine.GameObject::AddComponent<UnityEngine.AudioSource>()
inline AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* GameObject_AddComponent_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_m0E8EFDB9B3D8DF1ADE10C56D3168A9C1BA19BF14 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method)
{
	return ((  AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, const RuntimeMethod*))GameObject_AddComponent_TisRuntimeObject_m69B93700FACCF372F5753371C6E8FB780800B824_gshared)(__this, method);
}
// System.Void QFramework.AudioPlayer::CleanResources()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_CleanResources_mC72F4BF993C81EADE75AA24B40E3D39A3ACB4E63 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioPlayer::PlayAudioClip()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_PlayAudioClip_mE75B2E82ED39EC2C929277F8342A2749FFDE80F6 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) ;
// QFramework.AudioSearchKeys QFramework.AudioSearchKeys::Allocate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* AudioSearchKeys_Allocate_mCE7D3F47B99F8F3EDF78A16B3031F9D93EC906D1 (const RuntimeMethod* method) ;
// System.Void System.Action`2<System.Boolean,UnityEngine.AudioClip>::.ctor(System.Object,System.IntPtr)
inline void Action_2__ctor_mF42C97535261A64EA55EAADD09299B03C2D87D61 (Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD*, RuntimeObject*, intptr_t, const RuntimeMethod*))Action_2__ctor_m910EDF0572CB2F28E45048A2EB215E25296E58BD_gshared)(__this, ___0_object, ___1_method, method);
}
// System.Void QFramework.AudioSearchKeys::Recycle2Cache()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSearchKeys_Recycle2Cache_mD5F065BE8C2A1E01CFF642CF29FE8F969F0835E9 (AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioPlayer::Release()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_Release_mD740E38F2E434B0B6F98C8F93F19F498D6318595 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) ;
// System.Single QFramework.TimeItem::get_SortScore()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float TimeItem_get_SortScore_m77DBD8ADA7598C757D444823787D70A7FB5F18CF_inline (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) ;
// QFramework.Timer QFramework.Timer::get_Instance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* Timer_get_Instance_m20E57C957705CF1168B025EEAB98FAF69631694D (const RuntimeMethod* method) ;
// System.Single QFramework.Timer::get_CurrentScaleTime()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Timer_get_CurrentScaleTime_m7BA42A1AC216700628563A32EAF29F8571ADF3AA_inline (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, const RuntimeMethod* method) ;
// System.Void QFramework.TimeItem::Cancel()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem_Cancel_m37972167491A71932F518C5FECA2F73EE17CB62B (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.AudioSource::Pause()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Pause_m2C2A09359E8AA924FEADECC1AFEA519B3C915B26 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Int32>::.ctor(System.Object,System.IntPtr)
inline void Action_1__ctor_m1BA854F3F4319EA4A4294DDFDA21C395B8D0FF87 (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404*, RuntimeObject*, intptr_t, const RuntimeMethod*))Action_1__ctor_m1BA854F3F4319EA4A4294DDFDA21C395B8D0FF87_gshared)(__this, ___0_object, ___1_method, method);
}
// QFramework.TimeItem QFramework.Timer::Post2Scale(System.Action`1<System.Int32>,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* Timer_Post2Scale_m61CE9C095888EC19B9CB9B07CF2B81B446960B32 (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* ___0_callback, float ___1_delay, const RuntimeMethod* method) ;
// System.Void UnityEngine.AudioSource::Play()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Play_m95DF07111C61D0E0F00257A00384D31531D590C3 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioPlayer::UpdateVolume()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_UpdateVolume_m2D4666F4768B21FEF1D9B2AE1E34063F343552A9 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.AudioSource::set_volume(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_volume_mD902BBDBBDE0E3C148609BF3C05096148E90F2C0 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, float ___0_value, const RuntimeMethod* method) ;
// System.Void UnityEngine.Debug::LogError(System.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2 (RuntimeObject* ___0_message, const RuntimeMethod* method) ;
// System.Void UnityEngine.AudioSource::set_clip(UnityEngine.AudioClip)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_clip_mFF441895E274286C88D9C75ED5CA1B1B39528D70 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_value, const RuntimeMethod* method) ;
// System.Boolean QFramework.AudioPlayer::get_IsLoop()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool AudioPlayer_get_IsLoop_mC43549D534C455179A16FF1C2EDA241B117717E7_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.AudioSource::set_loop(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_set_loop_m834A590939D8456008C0F897FD80B0ECFFB7FE56 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, bool ___0_value, const RuntimeMethod* method) ;
// System.Single UnityEngine.AudioClip::get_length()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float AudioClip_get_length_m6102CB29AF65988797452E4D6E43D4788303873D (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* __this, const RuntimeMethod* method) ;
// QFramework.TimeItem QFramework.Timer::Post2Scale(System.Action`1<System.Int32>,System.Single,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* Timer_Post2Scale_m58F7D7517DF78356B41793A5FF4F930306E0117E (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* ___0_callback, float ___1_delay, int32_t ___2_repeat, const RuntimeMethod* method) ;
// System.Void QFramework.AudioPlayer::OnSoundPlayFinish(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_OnSoundPlayFinish_mEED80DC657DE5D9004B528A3E174D045EE01BDED (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, int32_t ___0_count, const RuntimeMethod* method) ;
// System.Boolean QFramework.AudioPlayer::get_UsedCache()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool AudioPlayer_get_UsedCache_m90781A61EB4E15DE4B973135C4D973D661351885_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) ;
// System.Void QFramework.AudioPlayer::Recycle2Cache()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_Recycle2Cache_mF99CD679BAF547DA43B1BA246824B9171E1EFBF5 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) ;
// UnityEngine.AudioClip UnityEngine.AudioSource::get_clip()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* AudioSource_get_clip_m4F5027066F9FC44B44192713142B0C277BB418FE (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) ;
// System.Void UnityEngine.AudioSource::Stop()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSource_Stop_m318F17F17A147C77FF6E0A5A7A6BE057DB90F537 (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* __this, const RuntimeMethod* method) ;
// System.Void QFramework.BindableProperty`1<System.Single>::UnRegister(System.Action`1<T>)
inline void BindableProperty_1_UnRegister_mDE5848741098D28A95EE63902771C3214DB883CA (BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* __this, Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* ___0_onValueChanged, const RuntimeMethod* method)
{
	((  void (*) (BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58*, Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A*, const RuntimeMethod*))BindableProperty_1_UnRegister_mDE5848741098D28A95EE63902771C3214DB883CA_gshared)(__this, ___0_onValueChanged, method);
}
// System.Boolean UnityEngine.Object::op_Inequality(UnityEngine.Object,UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602 (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_x, Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___1_y, const RuntimeMethod* method) ;
// System.Void UnityEngine.Object::Destroy(UnityEngine.Object)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Object_Destroy_mE97D0A766419A81296E8D4E5C23D01D3FE91ACBB (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* ___0_obj, const RuntimeMethod* method) ;
// QFramework.SafeObjectPool`1<T> QFramework.SafeObjectPool`1<QFramework.FluentMusicAPI>::get_Instance()
inline SafeObjectPool_1_t33C1137434D07D7A43A1A8C7D46F1C04B665E6F0* SafeObjectPool_1_get_Instance_m3795D49BEBB3B1FBDA78876A916F253627F999D5 (const RuntimeMethod* method)
{
	return ((  SafeObjectPool_1_t33C1137434D07D7A43A1A8C7D46F1C04B665E6F0* (*) (const RuntimeMethod*))SafeObjectPool_1_get_Instance_m0E3A8F4A5E333A96387C7017D66AE9621CD92A46_gshared)(method);
}
// System.Void QFramework.AudioKit::PlayMusic(UnityEngine.AudioClip,System.Boolean,System.Action,System.Action,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_PlayMusic_mA8FCD6EF87F4407C53D72C88A43A559C6440526B (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, bool ___1_loop, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___2_onBeganCallback, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___3_onEndCallback, float ___4_volume, const RuntimeMethod* method) ;
// QFramework.SafeObjectPool`1<T> QFramework.SafeObjectPool`1<QFramework.FluentSoundAPI>::get_Instance()
inline SafeObjectPool_1_t7FA207C564A34D5D5E4DE789675614FD0D0964A2* SafeObjectPool_1_get_Instance_m462FA7E647B9136571534928DB536BDFE6AB8B86 (const RuntimeMethod* method)
{
	return ((  SafeObjectPool_1_t7FA207C564A34D5D5E4DE789675614FD0D0964A2* (*) (const RuntimeMethod*))SafeObjectPool_1_get_Instance_m0E3A8F4A5E333A96387C7017D66AE9621CD92A46_gshared)(method);
}
// System.Void QFramework.FluentSoundAPI::Recycle2Cache()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FluentSoundAPI_Recycle2Cache_m99C6296EADD977F1C32FA60148A22A3F8312DF68 (FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* __this, const RuntimeMethod* method) ;
// T QFramework.Property`1<System.Int32>::get_Value()
inline int32_t Property_1_get_Value_mAB5065F7B5D8CB3B340114A5BB2953765536E0C7 (Property_1_tEA2D0DDF24CE19765ADE39EFBB400F89850D7DA8* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (Property_1_tEA2D0DDF24CE19765ADE39EFBB400F89850D7DA8*, const RuntimeMethod*))Property_1_get_Value_mAB5065F7B5D8CB3B340114A5BB2953765536E0C7_gshared)(__this, method);
}
// System.Void QFramework.Property`1<System.Int32>::set_Value(T)
inline void Property_1_set_Value_m314C5C5A72F5F0400ABFB00DFC1466A3434D26F3 (Property_1_tEA2D0DDF24CE19765ADE39EFBB400F89850D7DA8* __this, int32_t ___0_value, const RuntimeMethod* method)
{
	((  void (*) (Property_1_tEA2D0DDF24CE19765ADE39EFBB400F89850D7DA8*, int32_t, const RuntimeMethod*))Property_1_set_Value_m314C5C5A72F5F0400ABFB00DFC1466A3434D26F3_gshared)(__this, ___0_value, method);
}
// System.Void QFramework.Property`1<System.Int32>::.ctor()
inline void Property_1__ctor_m77CDCAA18D9FC2F380162C19E268B4B1956628E8 (Property_1_tEA2D0DDF24CE19765ADE39EFBB400F89850D7DA8* __this, const RuntimeMethod* method)
{
	((  void (*) (Property_1_tEA2D0DDF24CE19765ADE39EFBB400F89850D7DA8*, const RuntimeMethod*))Property_1__ctor_m77CDCAA18D9FC2F380162C19E268B4B1956628E8_gshared)(__this, method);
}
// QFramework.SafeObjectPool`1<T> QFramework.SafeObjectPool`1<QFramework.TimeItem>::get_Instance()
inline SafeObjectPool_1_tB9A3A3095B574142CF0139B7323BA66C80DCC342* SafeObjectPool_1_get_Instance_m27DE656011C461C719BEA3A05D455044BCA2757A (const RuntimeMethod* method)
{
	return ((  SafeObjectPool_1_tB9A3A3095B574142CF0139B7323BA66C80DCC342* (*) (const RuntimeMethod*))SafeObjectPool_1_get_Instance_m0E3A8F4A5E333A96387C7017D66AE9621CD92A46_gshared)(method);
}
// System.Void QFramework.TimeItem::Set(System.Action`1<System.Int32>,System.Single,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem_Set_m4FD3CF9D2583C7630D8B5CF5BA80C8C43DC31154 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* ___0_callback, float ___1_delayTime, int32_t ___2_repeatCount, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Int32>::Invoke(T)
inline void Action_1_Invoke_mAC3C34BA1905AB5B79E483CD9BB082B7D667F703_inline (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* __this, int32_t ___0_obj, const RuntimeMethod* method)
{
	((  void (*) (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404*, int32_t, const RuntimeMethod*))Action_1_Invoke_mAC3C34BA1905AB5B79E483CD9BB082B7D667F703_gshared_inline)(__this, ___0_obj, method);
}
// System.Boolean QFramework.TimeItem::get_IsEnable()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool TimeItem_get_IsEnable_m1E801E518495A9332F23BCC999918EF4E7F13A20_inline (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) ;
// System.Void QFramework.TimeItem::set_IsEnable(System.Boolean)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void TimeItem_set_IsEnable_m30F9D438BD5927F3B6CFBB9254EAF38AA6666834_inline (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, bool ___0_value, const RuntimeMethod* method) ;
// System.Void QFramework.TimeItem::set_HeapIndex(System.Int32)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void TimeItem_set_HeapIndex_mDBFF7F50CE32C2EBA90226B46F7297D6B15E3EDD_inline (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, int32_t ___0_value, const RuntimeMethod* method) ;
// T QFramework.MonoSingletonProperty`1<QFramework.Timer>::get_Instance()
inline Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* MonoSingletonProperty_1_get_Instance_m9EE6F4D4D2E389D777F50092E629D09BE4E90C76 (const RuntimeMethod* method)
{
	return ((  Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* (*) (const RuntimeMethod*))MonoSingletonProperty_1_get_Instance_mD0C88CCD4E7E334A10A2DF17478341DD46279967_gshared)(method);
}
// System.Void QFramework.BinaryHeap`1<QFramework.TimeItem>::Clear()
inline void BinaryHeap_1_Clear_mA0971C02BA613AC8B838BE7C9A470FD9D1F13EDB (BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* __this, const RuntimeMethod* method)
{
	((  void (*) (BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03*, const RuntimeMethod*))BinaryHeap_1_Clear_m6A0CBE3C113DCA81E2FBD07FA6C03905EB892BCD_gshared)(__this, method);
}
// System.Single UnityEngine.Time::get_unscaledTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Time_get_unscaledTime_mAF4040B858903E1325D1C65B8BF1AC61460B2503 (const RuntimeMethod* method) ;
// System.Single UnityEngine.Time::get_time()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Time_get_time_m3A271BB1B20041144AC5B7863B71AB1F0150374B (const RuntimeMethod* method) ;
// System.Void QFramework.Timer::set_CurrentScaleTime(System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Timer_set_CurrentScaleTime_m2429134D1ABDFB8CAAA020E62CD699B7598B7F2C_inline (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, float ___0_value, const RuntimeMethod* method) ;
// QFramework.TimeItem QFramework.TimeItem::Allocate(System.Action`1<System.Int32>,System.Single,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* TimeItem_Allocate_m8D72EDD32571CF80232A79522386B079D75B30D3 (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* ___0_callback, float ___1_delayTime, int32_t ___2_repeatCount, const RuntimeMethod* method) ;
// System.Void QFramework.Timer::Post2Scale(QFramework.TimeItem)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_Post2Scale_m6022E05EE6077C187236232544C3CFC8A601513A (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* ___0_item, const RuntimeMethod* method) ;
// System.Single QFramework.TimeItem::DelayTime()
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float TimeItem_DelayTime_m11860F52A7926A17AFB44D07A92618B709889A3E_inline (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) ;
// System.Void QFramework.TimeItem::set_SortScore(System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void TimeItem_set_SortScore_m3DA91B88557E9F0BC96DE66FC31183D0CB6FE265_inline (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, float ___0_value, const RuntimeMethod* method) ;
// System.Void QFramework.BinaryHeap`1<QFramework.TimeItem>::Insert(T)
inline void BinaryHeap_1_Insert_m0CE4B4F1F7097434EF0F96D95DD16C1592A72C8C (BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* __this, TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* ___0_element, const RuntimeMethod* method)
{
	((  void (*) (BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03*, TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8*, const RuntimeMethod*))BinaryHeap_1_Insert_m7ABB332CA3FA4532B0B919CD9F4D66240AD6C9AF_gshared)(__this, ___0_element, method);
}
// System.Void QFramework.Timer::UpdateMgr()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_UpdateMgr_mD44545C2811DD9428327A15EE6EA23930C1FF770 (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, const RuntimeMethod* method) ;
// T QFramework.BinaryHeap`1<QFramework.TimeItem>::Pop()
inline TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* BinaryHeap_1_Pop_mB0A91FAA9BBB3881F4743F07A49AC36C7B6796E1 (BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* __this, const RuntimeMethod* method)
{
	return ((  TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* (*) (BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03*, const RuntimeMethod*))BinaryHeap_1_Pop_mEF7862B11FC719D83AB8DB8D29A422203D798DCE_gshared)(__this, method);
}
// System.Void QFramework.TimeItem::Recycle2Cache()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem_Recycle2Cache_mDC21B7F37537C5C7265A9112DBF0A7F70B04D578 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) ;
// System.Void QFramework.TimeItem::OnTimeTick()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem_OnTimeTick_m44B75346F9E639D11F9BEE52A282E6958CCDC759 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) ;
// System.Boolean QFramework.TimeItem::NeedRepeat()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TimeItem_NeedRepeat_m53BFA90935C2F8AFE75AF8DC360B65B9CF798CB1 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) ;
// System.Void QFramework.Timer::Post2Really(QFramework.TimeItem)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_Post2Really_m7E718D8C05E9572FFE752EA28903911BC8263943 (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* ___0_item, const RuntimeMethod* method) ;
// T QFramework.BinaryHeap`1<QFramework.TimeItem>::Top()
inline TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* BinaryHeap_1_Top_m031CA8B0E52BBAD5A48F2732884CF6CC27C325E5 (BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* __this, const RuntimeMethod* method)
{
	return ((  TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* (*) (BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03*, const RuntimeMethod*))BinaryHeap_1_Top_m7EC63E2D65A9A75F608C02D09562C0BEF41FF986_gshared)(__this, method);
}
// System.Void QFramework.BinaryHeap`1<QFramework.TimeItem>::.ctor(System.Int32,QFramework.BinaryHeapSortMode)
inline void BinaryHeap_1__ctor_m3D6509830FB73EDAE5EAC9D72DF322E21B196B82 (BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* __this, int32_t ___0_minSize, int32_t ___1_sortMode, const RuntimeMethod* method)
{
	((  void (*) (BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03*, int32_t, int32_t, const RuntimeMethod*))BinaryHeap_1__ctor_m7C1B84DA73F74FAE45B9D55CC334FE4A690C3864_gshared)(__this, ___0_minSize, ___1_sortMode, method);
}
// System.Single UnityEngine.Time::get_realtimeSinceStartup()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Time_get_realtimeSinceStartup_m73B3CB73175D79A44333D59BB70F9EDE55EC9510 (const RuntimeMethod* method) ;
// System.Void QFramework.TimeExtend.Timer/<>c__DisplayClass15_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass15_0__ctor_mE7A02AE6DDC1583EB58402C7C6FEB12DCDFEA7DB (U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA* __this, const RuntimeMethod* method) ;
// QFramework.TimeExtend.TimerDriver QFramework.TimeExtend.TimerDriver::get_Get()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* TimerDriver_get_Get_m7485C3E16D307FB5ABAA222C83BF457B640BE373 (const RuntimeMethod* method) ;
// System.Single QFramework.TimeExtend.Timer::get_CurrentTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Timer_get_CurrentTime_m3B77F914DC06F585676CF36ADB0E4CCD63E0A2F0 (Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* __this, const RuntimeMethod* method) ;
// System.Void System.Predicate`1<QFramework.TimeExtend.Timer>::.ctor(System.Object,System.IntPtr)
inline void Predicate_1__ctor_mCF5C1738FF21D8836CA0F3017AD87E26CC807A92 (Predicate_1_t55DAAC67F486E666A2115F381B43C27ADF0B01CF* __this, RuntimeObject* ___0_object, intptr_t ___1_method, const RuntimeMethod* method)
{
	((  void (*) (Predicate_1_t55DAAC67F486E666A2115F381B43C27ADF0B01CF*, RuntimeObject*, intptr_t, const RuntimeMethod*))Predicate_1__ctor_m3E007299121A15DF80F4A210FF8C20E5DF688F20_gshared)(__this, ___0_object, ___1_method, method);
}
// System.Boolean System.Collections.Generic.List`1<QFramework.TimeExtend.Timer>::Exists(System.Predicate`1<T>)
inline bool List_1_Exists_mA5D007E602F98F4B2E9D7C5954BBAB42E8CB7888 (List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* __this, Predicate_1_t55DAAC67F486E666A2115F381B43C27ADF0B01CF* ___0_match, const RuntimeMethod* method)
{
	return ((  bool (*) (List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2*, Predicate_1_t55DAAC67F486E666A2115F381B43C27ADF0B01CF*, const RuntimeMethod*))List_1_Exists_mE124D5A8B431C8B9B4C77EA23AD8B4C543829643_gshared)(__this, ___0_match, method);
}
// System.Void UnityEngine.Debug::LogWarningFormat(System.String,System.Object[])
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Debug_LogWarningFormat_mD8224DEBCB6050F4E2BF55151F0C6A29B87DEFBC (String_t* ___0_format, ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* ___1_args, const RuntimeMethod* method) ;
// System.Single UnityEngine.Mathf::Clamp01(System.Single)
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Clamp01_mA7E048DBDA832D399A581BE4D6DED9FA44CE0F14_inline (float ___0_value, const RuntimeMethod* method) ;
// System.Void System.Action`1<System.Single>::Invoke(T)
inline void Action_1_Invoke_mA8F89FB04FEA0F48A4F22EC84B5F9ADB2914341F_inline (Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* __this, float ___0_obj, const RuntimeMethod* method)
{
	((  void (*) (Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A*, float, const RuntimeMethod*))Action_1_Invoke_mA8F89FB04FEA0F48A4F22EC84B5F9ADB2914341F_gshared_inline)(__this, ___0_obj, method);
}
// System.Void QFramework.TimeExtend.Timer::Stop()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_Stop_mDD7E5C13871F2A59DD2258C783C8F6FB2846C120 (Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* __this, const RuntimeMethod* method) ;
// System.Boolean System.Collections.Generic.List`1<QFramework.TimeExtend.Timer>::Contains(T)
inline bool List_1_Contains_m35660B24F32E8D50C67E6AE681D8E2084B5D8276 (List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* __this, Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* ___0_item, const RuntimeMethod* method)
{
	return ((  bool (*) (List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2*, Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4*, const RuntimeMethod*))List_1_Contains_m4C9139C2A6B23E9343D3F87807B32C6E2CFE660D_gshared)(__this, ___0_item, method);
}
// System.Boolean System.Collections.Generic.List`1<QFramework.TimeExtend.Timer>::Remove(T)
inline bool List_1_Remove_mE6E4B96921EE86B995707B091ABCBEEC1E894C22 (List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* __this, Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* ___0_item, const RuntimeMethod* method)
{
	return ((  bool (*) (List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2*, Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4*, const RuntimeMethod*))List_1_Remove_m4DFA48F4CEB9169601E75FC28517C5C06EFA5AD7_gshared)(__this, ___0_item, method);
}
// T System.Collections.Generic.List`1<QFramework.TimeExtend.Timer>::get_Item(System.Int32)
inline Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* List_1_get_Item_m593E51A37B3107E675E10D79C053FB4EE0857080 (List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* __this, int32_t ___0_index, const RuntimeMethod* method)
{
	return ((  Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* (*) (List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2*, int32_t, const RuntimeMethod*))List_1_get_Item_m33561245D64798C2AB07584C0EC4F240E4839A38_gshared)(__this, ___0_index, method);
}
// System.Void QFramework.TimeExtend.Timer::Update()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_Update_m71FC7B00FA77E5DC52D6F584B63946752B70DDCE (Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* __this, const RuntimeMethod* method) ;
// System.Int32 System.Collections.Generic.List`1<QFramework.TimeExtend.Timer>::get_Count()
inline int32_t List_1_get_Count_mA7DA63FD079A8939EE109E799F9A48EB03198C96_inline (List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* __this, const RuntimeMethod* method)
{
	return ((  int32_t (*) (List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2*, const RuntimeMethod*))List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline)(__this, method);
}
// System.Void System.Collections.Generic.List`1<QFramework.TimeExtend.Timer>::.ctor()
inline void List_1__ctor_m137F537B4FA09945E22CF1C0390E1A538D3D8F2C (List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* __this, const RuntimeMethod* method)
{
	((  void (*) (List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2*, const RuntimeMethod*))List_1__ctor_m7F078BB342729BDF11327FD89D7872265328F690_gshared)(__this, method);
}
// T UnityEngine.Object::FindObjectOfType<QFramework.TimeExtend.TimerDriver>()
inline TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* Object_FindObjectOfType_TisTimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_mA4C61CEE004298FBD022CDE9621561524631EE2E (const RuntimeMethod* method)
{
	return ((  TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* (*) (const RuntimeMethod*))Object_FindObjectOfType_TisRuntimeObject_m02DFBF011F3B59F777A5E521DB2A116DD496E968_gshared)(method);
}
// System.Void UnityEngine.GameObject::.ctor(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void GameObject__ctor_m37D512B05D292F954792225E6C6EEE95293A9B88 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, String_t* ___0_name, const RuntimeMethod* method) ;
// T UnityEngine.GameObject::AddComponent<QFramework.TimeExtend.TimerDriver>()
inline TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* GameObject_AddComponent_TisTimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_m6E1162BC6BB854F682DC3C069CCE84DCC39A8411 (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* __this, const RuntimeMethod* method)
{
	return ((  TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* (*) (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*, const RuntimeMethod*))GameObject_AddComponent_TisRuntimeObject_m69B93700FACCF372F5753371C6E8FB780800B824_gshared)(__this, method);
}
// System.Void QFramework.TimeExtend.Timer::UpdateAllTimer()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_UpdateAllTimer_m20568CB7C2E1B96B17452777B184DEAB1A94976D (const RuntimeMethod* method) ;
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
// QFramework.PlaySoundAction QFramework.PlaySoundAction::Allocate(System.String,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* PlaySoundAction_Allocate_m7684EAD9B8D292D4E9A89B0B13118565247F5AFB (String_t* ___0_soundName, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___1_onFinish, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AbstractAction_1_Allocate_m87F117921135FDFEECA4721CC49E3FA41F235F6D_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AbstractAction_1_t9A1EECE1BD7DD95509513E65E672298D45093CD3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// var playSoundAction = Allocate();
		il2cpp_codegen_runtime_class_init_inline(AbstractAction_1_t9A1EECE1BD7DD95509513E65E672298D45093CD3_il2cpp_TypeInfo_var);
		PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* L_0;
		L_0 = AbstractAction_1_Allocate_m87F117921135FDFEECA4721CC49E3FA41F235F6D(AbstractAction_1_Allocate_m87F117921135FDFEECA4721CC49E3FA41F235F6D_RuntimeMethod_var);
		// playSoundAction.mMode = Modes.ByName;
		PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* L_1 = L_0;
		NullCheck(L_1);
		L_1->___mMode_5 = 0;
		// playSoundAction.mSoundName = soundName;
		PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* L_2 = L_1;
		String_t* L_3 = ___0_soundName;
		NullCheck(L_2);
		L_2->___mSoundName_6 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&L_2->___mSoundName_6), (void*)L_3);
		// playSoundAction.mOnFinish = onFinish;
		PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* L_4 = L_2;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_5 = ___1_onFinish;
		NullCheck(L_4);
		L_4->___mOnFinish_8 = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&L_4->___mOnFinish_8), (void*)L_5);
		// return playSoundAction;
		return L_4;
	}
}
// QFramework.PlaySoundAction QFramework.PlaySoundAction::Allocate(UnityEngine.AudioClip,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* PlaySoundAction_Allocate_mE1701C1909915E2C3E474891ADBDA911BFFD8CEB (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_sound, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___1_onFinish, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AbstractAction_1_Allocate_m87F117921135FDFEECA4721CC49E3FA41F235F6D_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AbstractAction_1_t9A1EECE1BD7DD95509513E65E672298D45093CD3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// var playSoundAction = Allocate();
		il2cpp_codegen_runtime_class_init_inline(AbstractAction_1_t9A1EECE1BD7DD95509513E65E672298D45093CD3_il2cpp_TypeInfo_var);
		PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* L_0;
		L_0 = AbstractAction_1_Allocate_m87F117921135FDFEECA4721CC49E3FA41F235F6D(AbstractAction_1_Allocate_m87F117921135FDFEECA4721CC49E3FA41F235F6D_RuntimeMethod_var);
		// playSoundAction.mMode = Modes.ByClip;
		PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* L_1 = L_0;
		NullCheck(L_1);
		L_1->___mMode_5 = 1;
		// playSoundAction.mAudioClip = sound;
		PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* L_2 = L_1;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_3 = ___0_sound;
		NullCheck(L_2);
		L_2->___mAudioClip_7 = L_3;
		Il2CppCodeGenWriteBarrier((void**)(&L_2->___mAudioClip_7), (void*)L_3);
		// playSoundAction.mOnFinish = onFinish;
		PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* L_4 = L_2;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_5 = ___1_onFinish;
		NullCheck(L_4);
		L_4->___mOnFinish_8 = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&L_4->___mOnFinish_8), (void*)L_5);
		// return playSoundAction;
		return L_4;
	}
}
// System.Void QFramework.PlaySoundAction::OnStart()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlaySoundAction_OnStart_m00B7FAF49DD9145562C6ACAE2E66EF43C7D4FC8F (PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlaySoundAction_U3COnStartU3Eb__7_0_m92466F5143CFA6D9FE73B11EE6AF81613C7A1EDE_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlaySoundAction_U3COnStartU3Eb__7_1_mBCE202982D6EC8BCF75D7A4C8490EC6E49450EC0_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (mMode == Modes.ByName)
		int32_t L_0 = __this->___mMode_5;
		if (L_0)
		{
			goto IL_0027;
		}
	}
	{
		// AudioKit.PlaySound(mSoundName,callBack:player => this.Finish());
		String_t* L_1 = __this->___mSoundName_6;
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_2 = (Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219*)il2cpp_codegen_object_new(Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219_il2cpp_TypeInfo_var);
		NullCheck(L_2);
		Action_1__ctor_m3C560AF46A6955D28CF1A054DD88FD769247FDF0(L_2, __this, (intptr_t)((void*)PlaySoundAction_U3COnStartU3Eb__7_0_m92466F5143CFA6D9FE73B11EE6AF81613C7A1EDE_RuntimeMethod_var), NULL);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_3;
		L_3 = AudioKit_PlaySound_m9D36192E4F9A94A43432AE08EAB4852B703F6D29(L_1, (bool)0, L_2, (1.0f), NULL);
		return;
	}

IL_0027:
	{
		// else if (mMode == Modes.ByClip)
		int32_t L_4 = __this->___mMode_5;
		if ((!(((uint32_t)L_4) == ((uint32_t)1))))
		{
			goto IL_004e;
		}
	}
	{
		// AudioKit.PlaySound(mAudioClip,callBack:player => this.Finish());
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_5 = __this->___mAudioClip_7;
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_6 = (Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219*)il2cpp_codegen_object_new(Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219_il2cpp_TypeInfo_var);
		NullCheck(L_6);
		Action_1__ctor_m3C560AF46A6955D28CF1A054DD88FD769247FDF0(L_6, __this, (intptr_t)((void*)PlaySoundAction_U3COnStartU3Eb__7_1_mBCE202982D6EC8BCF75D7A4C8490EC6E49450EC0_RuntimeMethod_var), NULL);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_7;
		L_7 = AudioKit_PlaySound_m38FDA779D76D117AB6E58EA321E2DF0F1FA9B1E1(L_5, (bool)0, L_6, (1.0f), NULL);
	}

IL_004e:
	{
		// }
		return;
	}
}
// System.Void QFramework.PlaySoundAction::OnFinish()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlaySoundAction_OnFinish_mC5D8AFA5327ECFDFFA830BAACE400FBCD9433588 (PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* __this, const RuntimeMethod* method) 
{
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B2_0 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B1_0 = NULL;
	{
		// mOnFinish?.Invoke();
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = __this->___mOnFinish_8;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000b;
		}
	}
	{
		return;
	}

IL_000b:
	{
		NullCheck(G_B2_0);
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(G_B2_0, NULL);
		// }
		return;
	}
}
// System.Void QFramework.PlaySoundAction::OnDeinit()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlaySoundAction_OnDeinit_m9C6ABE2F0213BB9AAD865FAF7B2B5EAFF800075F (PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* __this, const RuntimeMethod* method) 
{
	{
		// mSoundName = null;
		__this->___mSoundName_6 = (String_t*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mSoundName_6), (void*)(String_t*)NULL);
		// mAudioClip = null;
		__this->___mAudioClip_7 = (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mAudioClip_7), (void*)(AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*)NULL);
		// mOnFinish = null;
		__this->___mOnFinish_8 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mOnFinish_8), (void*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL);
		// }
		return;
	}
}
// System.Void QFramework.PlaySoundAction::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlaySoundAction__ctor_mC7B9F18C9D7BCD72B455D60473E44E06911A1951 (PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AbstractAction_1__ctor_m59DED3CAB019783282FD5DAC55A1646BF2E0A8BC_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AbstractAction_1_t9A1EECE1BD7DD95509513E65E672298D45093CD3_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		il2cpp_codegen_runtime_class_init_inline(AbstractAction_1_t9A1EECE1BD7DD95509513E65E672298D45093CD3_il2cpp_TypeInfo_var);
		AbstractAction_1__ctor_m59DED3CAB019783282FD5DAC55A1646BF2E0A8BC(__this, AbstractAction_1__ctor_m59DED3CAB019783282FD5DAC55A1646BF2E0A8BC_RuntimeMethod_var);
		return;
	}
}
// System.Void QFramework.PlaySoundAction::<OnStart>b__7_0(QFramework.AudioPlayer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlaySoundAction_U3COnStartU3Eb__7_0_m92466F5143CFA6D9FE73B11EE6AF81613C7A1EDE (PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_player, const RuntimeMethod* method) 
{
	{
		// AudioKit.PlaySound(mSoundName,callBack:player => this.Finish());
		IActionExtensions_Finish_m6D37A7CE87DA297AF4186858DBCFB0DA67C98570(__this, NULL);
		return;
	}
}
// System.Void QFramework.PlaySoundAction::<OnStart>b__7_1(QFramework.AudioPlayer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void PlaySoundAction_U3COnStartU3Eb__7_1_mBCE202982D6EC8BCF75D7A4C8490EC6E49450EC0 (PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_player, const RuntimeMethod* method) 
{
	{
		// AudioKit.PlaySound(mAudioClip,callBack:player => this.Finish());
		IActionExtensions_Finish_m6D37A7CE87DA297AF4186858DBCFB0DA67C98570(__this, NULL);
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
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// QFramework.ISequence QFramework.PlaySoundExtension::PlaySound(QFramework.ISequence,System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* PlaySoundExtension_PlaySound_mA0E3AEA71A031942B8BB9831D7838FD97D2E1C33 (RuntimeObject* ___0_self, String_t* ___1_soundName, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ISequence_tFCEF100F3A0B8679A84CF63ED6D265A2607536D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return self.Append(QFramework.PlaySoundAction.Allocate(soundName));
		RuntimeObject* L_0 = ___0_self;
		String_t* L_1 = ___1_soundName;
		PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* L_2;
		L_2 = PlaySoundAction_Allocate_m7684EAD9B8D292D4E9A89B0B13118565247F5AFB(L_1, (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL, NULL);
		NullCheck(L_0);
		RuntimeObject* L_3;
		L_3 = InterfaceFuncInvoker1< RuntimeObject*, RuntimeObject* >::Invoke(0 /* QFramework.ISequence QFramework.ISequence::Append(QFramework.IAction) */, ISequence_tFCEF100F3A0B8679A84CF63ED6D265A2607536D4_il2cpp_TypeInfo_var, L_0, L_2);
		return L_3;
	}
}
// QFramework.ISequence QFramework.PlaySoundExtension::PlaySound(QFramework.ISequence,UnityEngine.AudioClip)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* PlaySoundExtension_PlaySound_mE63DA058E22D82556CB05A070EE5CB61966EAACD (RuntimeObject* ___0_self, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ISequence_tFCEF100F3A0B8679A84CF63ED6D265A2607536D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return self.Append(QFramework.PlaySoundAction.Allocate(clip));
		RuntimeObject* L_0 = ___0_self;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_1 = ___1_clip;
		PlaySoundAction_tF1059BEE8368B648AAA899CED9A4944CC2D9A909* L_2;
		L_2 = PlaySoundAction_Allocate_mE1701C1909915E2C3E474891ADBDA911BFFD8CEB(L_1, (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL, NULL);
		NullCheck(L_0);
		RuntimeObject* L_3;
		L_3 = InterfaceFuncInvoker1< RuntimeObject*, RuntimeObject* >::Invoke(0 /* QFramework.ISequence QFramework.ISequence::Append(QFramework.IAction) */, ISequence_tFCEF100F3A0B8679A84CF63ED6D265A2607536D4_il2cpp_TypeInfo_var, L_0, L_2);
		return L_3;
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
// QFramework.AudioPlayer QFramework.AudioKit::get_MusicPlayer()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioKit_get_MusicPlayer_m570B8BF0A07102B221CC2A2AE9632854FBC31940 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static AudioPlayer MusicPlayer => AudioManager.Instance.MusicPlayer;
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_0;
		L_0 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_0);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_1;
		L_1 = AudioManager_get_MusicPlayer_m8046BC707549A3B916F8C074D12CB7E6C79E27BA_inline(L_0, NULL);
		return L_1;
	}
}
// System.Void QFramework.AudioKit::PlayMusic(System.String,System.Boolean,System.Action,System.Action,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_PlayMusic_m024F70A37392A7DB77FD290AAA197DCF0FA7B8F6 (String_t* ___0_musicName, bool ___1_loop, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___2_onBeganCallback, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___3_onEndCallback, float ___4_volume, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralAB9EB91F16EF830015769D3B7CA3749AE4A7938F);
		s_Il2CppMethodInitialized = true;
	}
	AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* V_0 = NULL;
	{
		// AudioManager.Instance.CheckAudioListener();
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_0;
		L_0 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_0);
		AudioManager_CheckAudioListener_m886498C2B5C2966BB1776BA22ED94F0732BF7FA4(L_0, NULL);
		// var audioMgr = AudioManager.Instance;
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_1;
		L_1 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		V_0 = L_1;
		// audioMgr.CurrentMusicName = musicName;
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_2 = V_0;
		String_t* L_3 = ___0_musicName;
		NullCheck(L_2);
		AudioManager_set_CurrentMusicName_mE05C1458C0BCB217A49C8ABD589B4D4A8E77087C_inline(L_2, L_3, NULL);
		// Debug.Log(">>>>>> Start Play Music");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(_stringLiteralAB9EB91F16EF830015769D3B7CA3749AE4A7938F, NULL);
		// MusicPlayer.VolumeScale(volume)
		//     .OnStart(onBeganCallback)
		//     .SetAudio(audioMgr.gameObject, musicName, loop)
		//     .OnFinish(onEndCallback);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_4;
		L_4 = AudioKit_get_MusicPlayer_m570B8BF0A07102B221CC2A2AE9632854FBC31940(NULL);
		float L_5 = ___4_volume;
		NullCheck(L_4);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_6;
		L_6 = AudioPlayer_VolumeScale_mDD9640F3723126252B95FC8D6C3825E4E77FFC38(L_4, L_5, NULL);
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_7 = ___2_onBeganCallback;
		NullCheck(L_6);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_8;
		L_8 = AudioPlayer_OnStart_m6B06144CA7EE9C7117AE884B1EBBAEA727F6079D(L_6, L_7, NULL);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_9 = V_0;
		NullCheck(L_9);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_10;
		L_10 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(L_9, NULL);
		String_t* L_11 = ___0_musicName;
		bool L_12 = ___1_loop;
		NullCheck(L_8);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_13;
		L_13 = AudioPlayer_SetAudio_mB62B93B946B1ED12976423AA09E6ED70827AB5ED(L_8, L_10, L_11, L_12, NULL);
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_14 = ___3_onEndCallback;
		AudioKitOnFinishExtensions_OnFinish_mC950466C398349874B4E4EC22F908C665E7A739A(L_13, L_14, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioKit::StopMusic()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_StopMusic_m26066905E2B24264B08E3EF05032652F01327621 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// AudioManager.Instance.MusicPlayer.Stop();
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_0;
		L_0 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_0);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_1;
		L_1 = AudioManager_get_MusicPlayer_m8046BC707549A3B916F8C074D12CB7E6C79E27BA_inline(L_0, NULL);
		NullCheck(L_1);
		AudioPlayer_Stop_m76865190573766D55E4533B8AEA5AE7F8834E96B(L_1, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioKit::PauseMusic()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_PauseMusic_mC70F5C14FA099BA393EFA80C33B1EDA89666C8FD (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// AudioManager.Instance.MusicPlayer.Pause();
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_0;
		L_0 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_0);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_1;
		L_1 = AudioManager_get_MusicPlayer_m8046BC707549A3B916F8C074D12CB7E6C79E27BA_inline(L_0, NULL);
		NullCheck(L_1);
		AudioPlayer_Pause_mBFD7FA6BAA2AC4F968192F4F8A7FA8188EF865BC(L_1, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioKit::ResumeMusic()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_ResumeMusic_m41D980C7124218661BE860D29CDD06E4CC72D217 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// AudioManager.Instance.MusicPlayer.Resume();
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_0;
		L_0 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_0);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_1;
		L_1 = AudioManager_get_MusicPlayer_m8046BC707549A3B916F8C074D12CB7E6C79E27BA_inline(L_0, NULL);
		NullCheck(L_1);
		AudioPlayer_Resume_mC8F085A40D083FA2C0C2BE6D02492C58118F0522(L_1, NULL);
		// }
		return;
	}
}
// QFramework.AudioPlayer QFramework.AudioKit::get_VoicePlayer()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioKit_get_VoicePlayer_mBA33429E58724BA68DC47981A3F322547F573AF9 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static AudioPlayer VoicePlayer => AudioManager.Instance.VoicePlayer;
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_0;
		L_0 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_0);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_1;
		L_1 = AudioManager_get_VoicePlayer_m2FFBB17585A0BEEB38C67CF63A693A9F38C87D3D_inline(L_0, NULL);
		return L_1;
	}
}
// System.Void QFramework.AudioKit::PlayVoice(System.String,System.Boolean,System.Action,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_PlayVoice_m0DB27FC1DA555F0F196CD00346055B002B7DAC92 (String_t* ___0_voiceName, bool ___1_loop, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___2_onBeganCallback, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___3_onEndedCallback, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// var audioMgr = AudioManager.Instance;
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_0;
		L_0 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		// AudioManager.Instance.CheckAudioListener();
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_1;
		L_1 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_1);
		AudioManager_CheckAudioListener_m886498C2B5C2966BB1776BA22ED94F0732BF7FA4(L_1, NULL);
		// audioMgr.CurrentVoiceName = voiceName;
		String_t* L_2 = ___0_voiceName;
		NullCheck(L_0);
		AudioManager_set_CurrentVoiceName_m8203991273B51FA9A0FD8E9361C2D512E73CB6E4_inline(L_0, L_2, NULL);
		// if (!Settings.IsVoiceOn.Value)
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_3;
		L_3 = AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20_inline(NULL);
		NullCheck(L_3);
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_4;
		L_4 = AudioKitSettings_get_IsVoiceOn_m40198BDFF0BAC22A00FF9A7D42A90D9AF6BDF937_inline(L_3, NULL);
		NullCheck(L_4);
		bool L_5;
		L_5 = BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18(L_4, BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_RuntimeMethod_var);
		if (L_5)
		{
			goto IL_0027;
		}
	}
	{
		// return;
		return;
	}

IL_0027:
	{
		// VoicePlayer.OnStart(onBeganCallback);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_6;
		L_6 = AudioKit_get_VoicePlayer_mBA33429E58724BA68DC47981A3F322547F573AF9(NULL);
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_7 = ___2_onBeganCallback;
		NullCheck(L_6);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_8;
		L_8 = AudioPlayer_OnStart_m6B06144CA7EE9C7117AE884B1EBBAEA727F6079D(L_6, L_7, NULL);
		// VoicePlayer.SetAudio(AudioManager.Instance.gameObject, voiceName, loop);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_9;
		L_9 = AudioKit_get_VoicePlayer_mBA33429E58724BA68DC47981A3F322547F573AF9(NULL);
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_10;
		L_10 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_10);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_11;
		L_11 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(L_10, NULL);
		String_t* L_12 = ___0_voiceName;
		bool L_13 = ___1_loop;
		NullCheck(L_9);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_14;
		L_14 = AudioPlayer_SetAudio_mB62B93B946B1ED12976423AA09E6ED70827AB5ED(L_9, L_11, L_12, L_13, NULL);
		// VoicePlayer.OnFinish(onEndedCallback);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_15;
		L_15 = AudioKit_get_VoicePlayer_mBA33429E58724BA68DC47981A3F322547F573AF9(NULL);
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_16 = ___3_onEndedCallback;
		AudioKitOnFinishExtensions_OnFinish_mC950466C398349874B4E4EC22F908C665E7A739A(L_15, L_16, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioKit::PauseVoice()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_PauseVoice_m8AC95875EBD17BCF589486707E8B73788ABDE011 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// VoicePlayer.Pause();
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_0;
		L_0 = AudioKit_get_VoicePlayer_mBA33429E58724BA68DC47981A3F322547F573AF9(NULL);
		NullCheck(L_0);
		AudioPlayer_Pause_mBFD7FA6BAA2AC4F968192F4F8A7FA8188EF865BC(L_0, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioKit::ResumeVoice()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_ResumeVoice_mBCC4316C6000BA83507EA2F5E32088966410113C (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// VoicePlayer.Resume();
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_0;
		L_0 = AudioKit_get_VoicePlayer_mBA33429E58724BA68DC47981A3F322547F573AF9(NULL);
		NullCheck(L_0);
		AudioPlayer_Resume_mC8F085A40D083FA2C0C2BE6D02492C58118F0522(L_0, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioKit::StopVoice()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_StopVoice_mB98DA16AFE22E1BBD4C868273721AA2D89582391 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// VoicePlayer.Stop();
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_0;
		L_0 = AudioKit_get_VoicePlayer_mBA33429E58724BA68DC47981A3F322547F573AF9(NULL);
		NullCheck(L_0);
		AudioPlayer_Stop_m76865190573766D55E4533B8AEA5AE7F8834E96B(L_0, NULL);
		// }
		return;
	}
}
// System.Boolean QFramework.AudioKit::CanPlaySound(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioKit_CanPlaySound_m42B1801F93A991904D3EEF8C77AE2A56F01CA5A1 (String_t* ___0_soundName, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_Add_m2FE98C9C3763E31D7CB55207ED3A46B33BF64883_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_Clear_m841DBE29811833266CC127714688998A50D5F7CD_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_ContainsKey_mAEDD6BBEE1B37BC5E1D803803352FBE4CF4D3D7E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_TryGetValue_m835BB1E6EA8A8BF1242B51E28FD65B43FEF68E2A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_set_Item_m038480C0EC13713DBD89A53BE69FF0359501B4C2_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	{
		// if (PlaySoundMode == PlaySoundModes.EveryOne)
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		int32_t L_0 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___PlaySoundMode_1;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		// return true;
		return (bool)1;
	}

IL_0009:
	{
		// if (PlaySoundMode == PlaySoundModes.IgnoreSameSoundInGlobalFrames)
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		int32_t L_1 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___PlaySoundMode_1;
		if ((!(((uint32_t)L_1) == ((uint32_t)1))))
		{
			goto IL_0062;
		}
	}
	{
		// if (Time.frameCount - mGlobalFrameCount <= GlobalFrameCountForIgnoreSameSound)
		int32_t L_2;
		L_2 = Time_get_frameCount_m4A42E558A71301A216BDC49EC402D62F19C79667(NULL);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		int32_t L_3 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___mGlobalFrameCount_5;
		int32_t L_4 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___GlobalFrameCountForIgnoreSameSound_3;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_2, L_3))) > ((int32_t)L_4)))
		{
			goto IL_0040;
		}
	}
	{
		// if (mSoundFrameCountForName.ContainsKey(soundName))
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* L_5 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___mSoundFrameCountForName_4;
		String_t* L_6 = ___0_soundName;
		NullCheck(L_5);
		bool L_7;
		L_7 = Dictionary_2_ContainsKey_mAEDD6BBEE1B37BC5E1D803803352FBE4CF4D3D7E(L_5, L_6, Dictionary_2_ContainsKey_mAEDD6BBEE1B37BC5E1D803803352FBE4CF4D3D7E_RuntimeMethod_var);
		if (!L_7)
		{
			goto IL_0032;
		}
	}
	{
		// return false;
		return (bool)0;
	}

IL_0032:
	{
		// mSoundFrameCountForName.Add(soundName, 0);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* L_8 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___mSoundFrameCountForName_4;
		String_t* L_9 = ___0_soundName;
		NullCheck(L_8);
		Dictionary_2_Add_m2FE98C9C3763E31D7CB55207ED3A46B33BF64883(L_8, L_9, 0, Dictionary_2_Add_m2FE98C9C3763E31D7CB55207ED3A46B33BF64883_RuntimeMethod_var);
		goto IL_0060;
	}

IL_0040:
	{
		// mGlobalFrameCount = Time.frameCount;
		int32_t L_10;
		L_10 = Time_get_frameCount_m4A42E558A71301A216BDC49EC402D62F19C79667(NULL);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___mGlobalFrameCount_5 = L_10;
		// mSoundFrameCountForName.Clear();
		Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* L_11 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___mSoundFrameCountForName_4;
		NullCheck(L_11);
		Dictionary_2_Clear_m841DBE29811833266CC127714688998A50D5F7CD(L_11, Dictionary_2_Clear_m841DBE29811833266CC127714688998A50D5F7CD_RuntimeMethod_var);
		// mSoundFrameCountForName.Add(soundName, 0);
		Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* L_12 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___mSoundFrameCountForName_4;
		String_t* L_13 = ___0_soundName;
		NullCheck(L_12);
		Dictionary_2_Add_m2FE98C9C3763E31D7CB55207ED3A46B33BF64883(L_12, L_13, 0, Dictionary_2_Add_m2FE98C9C3763E31D7CB55207ED3A46B33BF64883_RuntimeMethod_var);
	}

IL_0060:
	{
		// return true;
		return (bool)1;
	}

IL_0062:
	{
		// if (PlaySoundMode == PlaySoundModes.IgnoreSameSoundInSoundFrames)
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		int32_t L_14 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___PlaySoundMode_1;
		if ((!(((uint32_t)L_14) == ((uint32_t)2))))
		{
			goto IL_00ab;
		}
	}
	{
		// if (mSoundFrameCountForName.TryGetValue(soundName, out var frames))
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* L_15 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___mSoundFrameCountForName_4;
		String_t* L_16 = ___0_soundName;
		NullCheck(L_15);
		bool L_17;
		L_17 = Dictionary_2_TryGetValue_m835BB1E6EA8A8BF1242B51E28FD65B43FEF68E2A(L_15, L_16, (&V_0), Dictionary_2_TryGetValue_m835BB1E6EA8A8BF1242B51E28FD65B43FEF68E2A_RuntimeMethod_var);
		if (!L_17)
		{
			goto IL_009b;
		}
	}
	{
		// if (Time.frameCount - frames <= SoundFrameCountForIgnoreSameSound)
		int32_t L_18;
		L_18 = Time_get_frameCount_m4A42E558A71301A216BDC49EC402D62F19C79667(NULL);
		int32_t L_19 = V_0;
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		int32_t L_20 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___SoundFrameCountForIgnoreSameSound_2;
		if ((((int32_t)((int32_t)il2cpp_codegen_subtract(L_18, L_19))) > ((int32_t)L_20)))
		{
			goto IL_0089;
		}
	}
	{
		// return false;
		return (bool)0;
	}

IL_0089:
	{
		// mSoundFrameCountForName[soundName] = Time.frameCount;
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* L_21 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___mSoundFrameCountForName_4;
		String_t* L_22 = ___0_soundName;
		int32_t L_23;
		L_23 = Time_get_frameCount_m4A42E558A71301A216BDC49EC402D62F19C79667(NULL);
		NullCheck(L_21);
		Dictionary_2_set_Item_m038480C0EC13713DBD89A53BE69FF0359501B4C2(L_21, L_22, L_23, Dictionary_2_set_Item_m038480C0EC13713DBD89A53BE69FF0359501B4C2_RuntimeMethod_var);
		goto IL_00ab;
	}

IL_009b:
	{
		// mSoundFrameCountForName.Add(soundName, Time.frameCount);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* L_24 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___mSoundFrameCountForName_4;
		String_t* L_25 = ___0_soundName;
		int32_t L_26;
		L_26 = Time_get_frameCount_m4A42E558A71301A216BDC49EC402D62F19C79667(NULL);
		NullCheck(L_24);
		Dictionary_2_Add_m2FE98C9C3763E31D7CB55207ED3A46B33BF64883(L_24, L_25, L_26, Dictionary_2_Add_m2FE98C9C3763E31D7CB55207ED3A46B33BF64883_RuntimeMethod_var);
	}

IL_00ab:
	{
		// return true;
		return (bool)1;
	}
}
// System.Void QFramework.AudioKit::SoundFinish(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_SoundFinish_m5A8751B115239ABA1523914682619A52B948FD2D (String_t* ___0_soundName, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_Remove_m0C90DB78F4134CDEB001F338AA2745F8D9651CAC_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (PlaySoundMode == PlaySoundModes.IgnoreSameSoundInSoundFrames)
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		int32_t L_0 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___PlaySoundMode_1;
		if ((!(((uint32_t)L_0) == ((uint32_t)2))))
		{
			goto IL_0014;
		}
	}
	{
		// mSoundFrameCountForName.Remove(soundName);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* L_1 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___mSoundFrameCountForName_4;
		String_t* L_2 = ___0_soundName;
		NullCheck(L_1);
		bool L_3;
		L_3 = Dictionary_2_Remove_m0C90DB78F4134CDEB001F338AA2745F8D9651CAC(L_1, L_2, Dictionary_2_Remove_m0C90DB78F4134CDEB001F338AA2745F8D9651CAC_RuntimeMethod_var);
	}

IL_0014:
	{
		// }
		return;
	}
}
// QFramework.AudioPlayer QFramework.AudioKit::PlaySound(System.String,System.Boolean,System.Action`1<QFramework.AudioPlayer>,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioKit_PlaySound_m9D36192E4F9A94A43432AE08EAB4852B703F6D29 (String_t* ___0_soundName, bool ___1_loop, Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* ___2_callBack, float ___3_volume, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass21_0_U3CPlaySoundU3Eb__0_mDA28D0DC610632C2F0C62AE0ECA419368839AE9C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* L_0 = (U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass21_0__ctor_m842ED89D76A35F5192C21F17A9D41BFF9CB7FB30(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* L_1 = V_0;
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_2 = ___2_callBack;
		NullCheck(L_1);
		L_1->___callBack_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___callBack_0), (void*)L_2);
		U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* L_3 = V_0;
		String_t* L_4 = ___0_soundName;
		NullCheck(L_3);
		L_3->___soundName_2 = L_4;
		Il2CppCodeGenWriteBarrier((void**)(&L_3->___soundName_2), (void*)L_4);
		// AudioManager.Instance.CheckAudioListener();
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_5;
		L_5 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_5);
		AudioManager_CheckAudioListener_m886498C2B5C2966BB1776BA22ED94F0732BF7FA4(L_5, NULL);
		// if (!Settings.IsSoundOn.Value) return null;
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_6;
		L_6 = AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20_inline(NULL);
		NullCheck(L_6);
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_7;
		L_7 = AudioKitSettings_get_IsSoundOn_m16E5D82CD5CE9BDAA868820A75FA970239276A85_inline(L_6, NULL);
		NullCheck(L_7);
		bool L_8;
		L_8 = BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18(L_7, BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_RuntimeMethod_var);
		if (L_8)
		{
			goto IL_0031;
		}
	}
	{
		// if (!Settings.IsSoundOn.Value) return null;
		return (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59*)NULL;
	}

IL_0031:
	{
		// if (!CanPlaySound(soundName)) return null;
		U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* L_9 = V_0;
		NullCheck(L_9);
		String_t* L_10 = L_9->___soundName_2;
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		bool L_11;
		L_11 = AudioKit_CanPlaySound_m42B1801F93A991904D3EEF8C77AE2A56F01CA5A1(L_10, NULL);
		if (L_11)
		{
			goto IL_0040;
		}
	}
	{
		// if (!CanPlaySound(soundName)) return null;
		return (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59*)NULL;
	}

IL_0040:
	{
		// var soundPlayer = AudioPlayer.Allocate(Settings.SoundVolume);
		U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* L_12 = V_0;
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_13;
		L_13 = AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20_inline(NULL);
		NullCheck(L_13);
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_14;
		L_14 = AudioKitSettings_get_SoundVolume_mD628B2C76B4EE61E1BF923E41126A63B0F3B897A_inline(L_13, NULL);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_15;
		L_15 = AudioPlayer_Allocate_m7E4B1CB936A2FB00F782DA45373DF120B5C35646(L_14, NULL);
		NullCheck(L_12);
		L_12->___soundPlayer_1 = L_15;
		Il2CppCodeGenWriteBarrier((void**)(&L_12->___soundPlayer_1), (void*)L_15);
		// soundPlayer.VolumeScale(volume)
		//     .SetAudio(AudioManager.Instance.gameObject, soundName, loop);
		U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* L_16 = V_0;
		NullCheck(L_16);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_17 = L_16->___soundPlayer_1;
		float L_18 = ___3_volume;
		NullCheck(L_17);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_19;
		L_19 = AudioPlayer_VolumeScale_mDD9640F3723126252B95FC8D6C3825E4E77FFC38(L_17, L_18, NULL);
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_20;
		L_20 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_20);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_21;
		L_21 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(L_20, NULL);
		U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* L_22 = V_0;
		NullCheck(L_22);
		String_t* L_23 = L_22->___soundName_2;
		bool L_24 = ___1_loop;
		NullCheck(L_19);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_25;
		L_25 = AudioPlayer_SetAudio_mB62B93B946B1ED12976423AA09E6ED70827AB5ED(L_19, L_21, L_23, L_24, NULL);
		// soundPlayer.OnFinish(() =>
		// {
		//     callBack?.Invoke(soundPlayer);
		//     AudioManager.Instance.RemoveSoundPlayerFromPool(soundPlayer);
		//     SoundFinish(soundName);
		// });
		U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* L_26 = V_0;
		NullCheck(L_26);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_27 = L_26->___soundPlayer_1;
		U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* L_28 = V_0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_29 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		NullCheck(L_29);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_29, L_28, (intptr_t)((void*)U3CU3Ec__DisplayClass21_0_U3CPlaySoundU3Eb__0_mDA28D0DC610632C2F0C62AE0ECA419368839AE9C_RuntimeMethod_var), NULL);
		AudioKitOnFinishExtensions_OnFinish_mC950466C398349874B4E4EC22F908C665E7A739A(L_27, L_29, NULL);
		// AudioManager.Instance.AddSoundPlayer2Pool(soundPlayer);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_30;
		L_30 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* L_31 = V_0;
		NullCheck(L_31);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_32 = L_31->___soundPlayer_1;
		NullCheck(L_30);
		AudioManager_AddSoundPlayer2Pool_m65FCF7B9091A32D9C58119B23787DDFEBB820B2E(L_30, L_32, NULL);
		// return soundPlayer;
		U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* L_33 = V_0;
		NullCheck(L_33);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_34 = L_33->___soundPlayer_1;
		return L_34;
	}
}
// System.Void QFramework.AudioKit::StopAllSound()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_StopAllSound_m7D7141574943592C9E572CAF493180065E80AD75 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec_U3CStopAllSoundU3Eb__22_0_m90BB3B9C70D28CFE509799F933282F40E10125BB_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* G_B2_0 = NULL;
	AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* G_B2_1 = NULL;
	Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* G_B1_0 = NULL;
	AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* G_B1_1 = NULL;
	{
		// AudioManager.Instance.ForEachAllSound(player => player.Stop());
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_0;
		L_0 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		il2cpp_codegen_runtime_class_init_inline(U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_il2cpp_TypeInfo_var);
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_1 = ((U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_il2cpp_TypeInfo_var))->___U3CU3E9__22_0_1;
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_2 = L_1;
		G_B1_0 = L_2;
		G_B1_1 = L_0;
		if (L_2)
		{
			G_B2_0 = L_2;
			G_B2_1 = L_0;
			goto IL_0024;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_il2cpp_TypeInfo_var);
		U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC* L_3 = ((U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_il2cpp_TypeInfo_var))->___U3CU3E9_0;
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_4 = (Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219*)il2cpp_codegen_object_new(Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		Action_1__ctor_m3C560AF46A6955D28CF1A054DD88FD769247FDF0(L_4, L_3, (intptr_t)((void*)U3CU3Ec_U3CStopAllSoundU3Eb__22_0_m90BB3B9C70D28CFE509799F933282F40E10125BB_RuntimeMethod_var), NULL);
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_5 = L_4;
		((U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_il2cpp_TypeInfo_var))->___U3CU3E9__22_0_1 = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&((U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_il2cpp_TypeInfo_var))->___U3CU3E9__22_0_1), (void*)L_5);
		G_B2_0 = L_5;
		G_B2_1 = G_B1_1;
	}

IL_0024:
	{
		NullCheck(G_B2_1);
		AudioManager_ForEachAllSound_m937C7CCCD694ECFFC9A32A0F90389A389B046EB2(G_B2_1, G_B2_0, NULL);
		// AudioManager.Instance.ClearAllPlayingSound();
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_6;
		L_6 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_6);
		AudioManager_ClearAllPlayingSound_mF5A45B5E7FC9FA2AAB1ECA1A5CEA41F812304278(L_6, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioKit::PlayMusic(UnityEngine.AudioClip,System.Boolean,System.Action,System.Action,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_PlayMusic_mA8FCD6EF87F4407C53D72C88A43A559C6440526B (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, bool ___1_loop, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___2_onBeganCallback, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___3_onEndCallback, float ___4_volume, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral7A9D14B94D28FB9E5C1133505C5CFB7767D6C55F);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralAB9EB91F16EF830015769D3B7CA3749AE4A7938F);
		s_Il2CppMethodInitialized = true;
	}
	AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* V_0 = NULL;
	int32_t V_1 = 0;
	{
		// AudioManager.Instance.CheckAudioListener();
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_0;
		L_0 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_0);
		AudioManager_CheckAudioListener_m886498C2B5C2966BB1776BA22ED94F0732BF7FA4(L_0, NULL);
		// var audioMgr = AudioManager.Instance;
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_1;
		L_1 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		V_0 = L_1;
		// audioMgr.CurrentMusicName = "music" + clip.GetHashCode();
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_2 = V_0;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_3 = ___0_clip;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = VirtualFuncInvoker0< int32_t >::Invoke(2 /* System.Int32 System.Object::GetHashCode() */, L_3);
		V_1 = L_4;
		String_t* L_5;
		L_5 = Int32_ToString_m030E01C24E294D6762FB0B6F37CB541581F55CA5((&V_1), NULL);
		String_t* L_6;
		L_6 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral7A9D14B94D28FB9E5C1133505C5CFB7767D6C55F, L_5, NULL);
		NullCheck(L_2);
		AudioManager_set_CurrentMusicName_mE05C1458C0BCB217A49C8ABD589B4D4A8E77087C_inline(L_2, L_6, NULL);
		// Debug.Log(">>>>>> Start Play Music");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(_stringLiteralAB9EB91F16EF830015769D3B7CA3749AE4A7938F, NULL);
		// MusicPlayer.VolumeScale(volume)
		//     .OnStart(onBeganCallback);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_7;
		L_7 = AudioKit_get_MusicPlayer_m570B8BF0A07102B221CC2A2AE9632854FBC31940(NULL);
		float L_8 = ___4_volume;
		NullCheck(L_7);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_9;
		L_9 = AudioPlayer_VolumeScale_mDD9640F3723126252B95FC8D6C3825E4E77FFC38(L_7, L_8, NULL);
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_10 = ___2_onBeganCallback;
		NullCheck(L_9);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_11;
		L_11 = AudioPlayer_OnStart_m6B06144CA7EE9C7117AE884B1EBBAEA727F6079D(L_9, L_10, NULL);
		// MusicPlayer.SetAudioExt(audioMgr.gameObject, clip, audioMgr.CurrentMusicName, loop);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_12;
		L_12 = AudioKit_get_MusicPlayer_m570B8BF0A07102B221CC2A2AE9632854FBC31940(NULL);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_13 = V_0;
		NullCheck(L_13);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_14;
		L_14 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(L_13, NULL);
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_15 = ___0_clip;
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_16 = V_0;
		NullCheck(L_16);
		String_t* L_17;
		L_17 = AudioManager_get_CurrentMusicName_m46E7270EA32A4B0CD9B4261AF7CB78A75C6444FB_inline(L_16, NULL);
		bool L_18 = ___1_loop;
		NullCheck(L_12);
		AudioPlayer_SetAudioExt_m159E924BA175BD34E5A784D5B274816B100994C7(L_12, L_14, L_15, L_17, L_18, NULL);
		// MusicPlayer.OnFinish(onEndCallback);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_19;
		L_19 = AudioKit_get_MusicPlayer_m570B8BF0A07102B221CC2A2AE9632854FBC31940(NULL);
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_20 = ___3_onEndCallback;
		AudioKitOnFinishExtensions_OnFinish_mC950466C398349874B4E4EC22F908C665E7A739A(L_19, L_20, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioKit::PlayVoice(UnityEngine.AudioClip,System.Boolean,System.Action,System.Action,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit_PlayVoice_mC98D5314AA67D244E8E2F0C53790D9F33BB5422C (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, bool ___1_loop, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___2_onBeganCallback, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___3_onEndedCallback, float ___4_volumeScale, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralBAD4A6E573B068D6167F13578714BA76E87F09CB);
		s_Il2CppMethodInitialized = true;
	}
	AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* V_0 = NULL;
	int32_t V_1 = 0;
	{
		// AudioManager.Instance.CheckAudioListener();
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_0;
		L_0 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_0);
		AudioManager_CheckAudioListener_m886498C2B5C2966BB1776BA22ED94F0732BF7FA4(L_0, NULL);
		// var audioMgr = AudioManager.Instance;
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_1;
		L_1 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		V_0 = L_1;
		// audioMgr.CurrentVoiceName = "voice" + clip.GetHashCode();
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_2 = V_0;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_3 = ___0_clip;
		NullCheck(L_3);
		int32_t L_4;
		L_4 = VirtualFuncInvoker0< int32_t >::Invoke(2 /* System.Int32 System.Object::GetHashCode() */, L_3);
		V_1 = L_4;
		String_t* L_5;
		L_5 = Int32_ToString_m030E01C24E294D6762FB0B6F37CB541581F55CA5((&V_1), NULL);
		String_t* L_6;
		L_6 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralBAD4A6E573B068D6167F13578714BA76E87F09CB, L_5, NULL);
		NullCheck(L_2);
		AudioManager_set_CurrentVoiceName_m8203991273B51FA9A0FD8E9361C2D512E73CB6E4_inline(L_2, L_6, NULL);
		// if (!Settings.IsVoiceOn.Value)
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_7;
		L_7 = AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20_inline(NULL);
		NullCheck(L_7);
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_8;
		L_8 = AudioKitSettings_get_IsVoiceOn_m40198BDFF0BAC22A00FF9A7D42A90D9AF6BDF937_inline(L_7, NULL);
		NullCheck(L_8);
		bool L_9;
		L_9 = BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18(L_8, BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_RuntimeMethod_var);
		if (L_9)
		{
			goto IL_0040;
		}
	}
	{
		// return;
		return;
	}

IL_0040:
	{
		// VoicePlayer.VolumeScale(volumeScale)
		//     .OnStart(onBeganCallback);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_10;
		L_10 = AudioKit_get_VoicePlayer_mBA33429E58724BA68DC47981A3F322547F573AF9(NULL);
		float L_11 = ___4_volumeScale;
		NullCheck(L_10);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_12;
		L_12 = AudioPlayer_VolumeScale_mDD9640F3723126252B95FC8D6C3825E4E77FFC38(L_10, L_11, NULL);
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_13 = ___2_onBeganCallback;
		NullCheck(L_12);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_14;
		L_14 = AudioPlayer_OnStart_m6B06144CA7EE9C7117AE884B1EBBAEA727F6079D(L_12, L_13, NULL);
		// VoicePlayer.SetAudioExt(AudioManager.Instance.gameObject, clip, audioMgr.CurrentVoiceName, loop);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_15;
		L_15 = AudioKit_get_VoicePlayer_mBA33429E58724BA68DC47981A3F322547F573AF9(NULL);
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_16;
		L_16 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_16);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_17;
		L_17 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(L_16, NULL);
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_18 = ___0_clip;
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_19 = V_0;
		NullCheck(L_19);
		String_t* L_20;
		L_20 = AudioManager_get_CurrentVoiceName_m2A6026179BDD9F9E07A287B6935786E0CCF24FA4_inline(L_19, NULL);
		bool L_21 = ___1_loop;
		NullCheck(L_15);
		AudioPlayer_SetAudioExt_m159E924BA175BD34E5A784D5B274816B100994C7(L_15, L_17, L_18, L_20, L_21, NULL);
		// VoicePlayer.OnFinish(onEndedCallback);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_22;
		L_22 = AudioKit_get_VoicePlayer_mBA33429E58724BA68DC47981A3F322547F573AF9(NULL);
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_23 = ___3_onEndedCallback;
		AudioKitOnFinishExtensions_OnFinish_mC950466C398349874B4E4EC22F908C665E7A739A(L_22, L_23, NULL);
		// }
		return;
	}
}
// QFramework.AudioPlayer QFramework.AudioKit::PlaySound(UnityEngine.AudioClip,System.Boolean,System.Action`1<QFramework.AudioPlayer>,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioKit_PlaySound_m38FDA779D76D117AB6E58EA321E2DF0F1FA9B1E1 (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, bool ___1_loop, Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* ___2_callBack, float ___3_volume, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass25_0_U3CPlaySoundU3Eb__0_m8D85FB5C48586757CCB3D6BD15B129DC3F0038D6_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral240BE9B5E609F0DD8657150691504C4DE92A0925);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51* V_0 = NULL;
	int32_t V_1 = 0;
	{
		U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51* L_0 = (U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass25_0__ctor_m74B5819BBCF04D3324DF32EAABD3064AAC6A16C2(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51* L_1 = V_0;
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_2 = ___2_callBack;
		NullCheck(L_1);
		L_1->___callBack_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___callBack_0), (void*)L_2);
		// AudioManager.Instance.CheckAudioListener();
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_3;
		L_3 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_3);
		AudioManager_CheckAudioListener_m886498C2B5C2966BB1776BA22ED94F0732BF7FA4(L_3, NULL);
		// if (!Settings.IsSoundOn.Value) return null;
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_4;
		L_4 = AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20_inline(NULL);
		NullCheck(L_4);
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_5;
		L_5 = AudioKitSettings_get_IsSoundOn_m16E5D82CD5CE9BDAA868820A75FA970239276A85_inline(L_4, NULL);
		NullCheck(L_5);
		bool L_6;
		L_6 = BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18(L_5, BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_RuntimeMethod_var);
		if (L_6)
		{
			goto IL_002a;
		}
	}
	{
		// if (!Settings.IsSoundOn.Value) return null;
		return (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59*)NULL;
	}

IL_002a:
	{
		// if (!CanPlaySound(clip.name)) return null;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_7 = ___0_clip;
		NullCheck(L_7);
		String_t* L_8;
		L_8 = Object_get_name_mAC2F6B897CF1303BA4249B4CB55271AFACBB6392(L_7, NULL);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		bool L_9;
		L_9 = AudioKit_CanPlaySound_m42B1801F93A991904D3EEF8C77AE2A56F01CA5A1(L_8, NULL);
		if (L_9)
		{
			goto IL_0039;
		}
	}
	{
		// if (!CanPlaySound(clip.name)) return null;
		return (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59*)NULL;
	}

IL_0039:
	{
		// var soundPlayer = AudioPlayer.Allocate(Settings.SoundVolume);
		U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51* L_10 = V_0;
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_11;
		L_11 = AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20_inline(NULL);
		NullCheck(L_11);
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_12;
		L_12 = AudioKitSettings_get_SoundVolume_mD628B2C76B4EE61E1BF923E41126A63B0F3B897A_inline(L_11, NULL);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_13;
		L_13 = AudioPlayer_Allocate_m7E4B1CB936A2FB00F782DA45373DF120B5C35646(L_12, NULL);
		NullCheck(L_10);
		L_10->___soundPlayer_1 = L_13;
		Il2CppCodeGenWriteBarrier((void**)(&L_10->___soundPlayer_1), (void*)L_13);
		// soundPlayer.VolumeScale(volume)
		//     .SetAudioExt(AudioManager.Instance.gameObject, clip, "sound" + clip.GetHashCode(), loop);
		U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51* L_14 = V_0;
		NullCheck(L_14);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_15 = L_14->___soundPlayer_1;
		float L_16 = ___3_volume;
		NullCheck(L_15);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_17;
		L_17 = AudioPlayer_VolumeScale_mDD9640F3723126252B95FC8D6C3825E4E77FFC38(L_15, L_16, NULL);
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_18;
		L_18 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_18);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_19;
		L_19 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(L_18, NULL);
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_20 = ___0_clip;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_21 = ___0_clip;
		NullCheck(L_21);
		int32_t L_22;
		L_22 = VirtualFuncInvoker0< int32_t >::Invoke(2 /* System.Int32 System.Object::GetHashCode() */, L_21);
		V_1 = L_22;
		String_t* L_23;
		L_23 = Int32_ToString_m030E01C24E294D6762FB0B6F37CB541581F55CA5((&V_1), NULL);
		String_t* L_24;
		L_24 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteral240BE9B5E609F0DD8657150691504C4DE92A0925, L_23, NULL);
		bool L_25 = ___1_loop;
		NullCheck(L_17);
		AudioPlayer_SetAudioExt_m159E924BA175BD34E5A784D5B274816B100994C7(L_17, L_19, L_20, L_24, L_25, NULL);
		// soundPlayer.OnFinish(() =>
		// {
		//     callBack?.Invoke(soundPlayer);
		//     AudioManager.Instance.RemoveSoundPlayerFromPool(soundPlayer);
		// });
		U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51* L_26 = V_0;
		NullCheck(L_26);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_27 = L_26->___soundPlayer_1;
		U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51* L_28 = V_0;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_29 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		NullCheck(L_29);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_29, L_28, (intptr_t)((void*)U3CU3Ec__DisplayClass25_0_U3CPlaySoundU3Eb__0_m8D85FB5C48586757CCB3D6BD15B129DC3F0038D6_RuntimeMethod_var), NULL);
		AudioKitOnFinishExtensions_OnFinish_mC950466C398349874B4E4EC22F908C665E7A739A(L_27, L_29, NULL);
		// AudioManager.Instance.AddSoundPlayer2Pool(soundPlayer);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_30;
		L_30 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51* L_31 = V_0;
		NullCheck(L_31);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_32 = L_31->___soundPlayer_1;
		NullCheck(L_30);
		AudioManager_AddSoundPlayer2Pool_m65FCF7B9091A32D9C58119B23787DDFEBB820B2E(L_30, L_32, NULL);
		// return soundPlayer;
		U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51* L_33 = V_0;
		NullCheck(L_33);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_34 = L_33->___soundPlayer_1;
		return L_34;
	}
}
// QFramework.AudioKitSettings QFramework.AudioKit::get_Settings()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static AudioKitSettings Settings { get; } = new AudioKitSettings();
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_0 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___U3CSettingsU3Ek__BackingField_6;
		return L_0;
	}
}
// QFramework.FluentMusicAPI QFramework.AudioKit::Music()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* AudioKit_Music_m4A559164F9E88AED5A277453E38A49728C10D73D (const RuntimeMethod* method) 
{
	{
		// public static FluentMusicAPI Music() => FluentMusicAPI.Allocate();
		FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* L_0;
		L_0 = FluentMusicAPI_Allocate_m47EA60F843D95341E109C9D8FDDC8436B003579E(NULL);
		return L_0;
	}
}
// QFramework.FluentSoundAPI QFramework.AudioKit::Sound()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* AudioKit_Sound_m2898207D45669A2A06927EE977D74C6AFAF8274C (const RuntimeMethod* method) 
{
	{
		// public static FluentSoundAPI Sound() => FluentSoundAPI.Allocate();
		FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* L_0;
		L_0 = FluentSoundAPI_Allocate_mCB696368A37809648CBB30613D38F8CE94DF18A0(NULL);
		return L_0;
	}
}
// System.Void QFramework.AudioKit::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit__ctor_m2AA966BBE696D40509B70769CD539B0C734A3A93 (AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void QFramework.AudioKit::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKit__cctor_m4A0CE87F6E1163EAD98761E28CBF85A10A217880 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKitConfig_t0518724CE4F14A672627665BE74D533751686A54_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2__ctor_mA3C3860EDE2CDD08BBD68C389377BC89D029D968_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static AudioKitConfig Config = new AudioKitConfig();
		AudioKitConfig_t0518724CE4F14A672627665BE74D533751686A54* L_0 = (AudioKitConfig_t0518724CE4F14A672627665BE74D533751686A54*)il2cpp_codegen_object_new(AudioKitConfig_t0518724CE4F14A672627665BE74D533751686A54_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		AudioKitConfig__ctor_m4209BF93AC62060B70335750DE6D083C7699565E(L_0, NULL);
		((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___Config_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___Config_0), (void*)L_0);
		// public static PlaySoundModes PlaySoundMode = PlaySoundModes.EveryOne;
		((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___PlaySoundMode_1 = 0;
		// public static int SoundFrameCountForIgnoreSameSound = 10;
		((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___SoundFrameCountForIgnoreSameSound_2 = ((int32_t)10);
		// public static int GlobalFrameCountForIgnoreSameSound = 10;
		((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___GlobalFrameCountForIgnoreSameSound_3 = ((int32_t)10);
		// private static Dictionary<string, int> mSoundFrameCountForName = new Dictionary<string, int>();
		Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588* L_1 = (Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588*)il2cpp_codegen_object_new(Dictionary_2_t5C8F46F5D57502270DD9E1DA8303B23C7FE85588_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		Dictionary_2__ctor_mA3C3860EDE2CDD08BBD68C389377BC89D029D968(L_1, Dictionary_2__ctor_mA3C3860EDE2CDD08BBD68C389377BC89D029D968_RuntimeMethod_var);
		((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___mSoundFrameCountForName_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___mSoundFrameCountForName_4), (void*)L_1);
		// public static AudioKitSettings Settings { get; } = new AudioKitSettings();
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_2 = (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8*)il2cpp_codegen_object_new(AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8_il2cpp_TypeInfo_var);
		NullCheck(L_2);
		AudioKitSettings__ctor_m0D57B7E51C1244C01164A711F52CA0FB77DD88F1(L_2, NULL);
		((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___U3CSettingsU3Ek__BackingField_6 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___U3CSettingsU3Ek__BackingField_6), (void*)L_2);
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
#ifdef __clang__
#pragma clang diagnostic pop
#endif
#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif
// System.Void QFramework.AudioKit/<>c__DisplayClass21_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass21_0__ctor_m842ED89D76A35F5192C21F17A9D41BFF9CB7FB30 (U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void QFramework.AudioKit/<>c__DisplayClass21_0::<PlaySound>b__0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass21_0_U3CPlaySoundU3Eb__0_mDA28D0DC610632C2F0C62AE0ECA419368839AE9C (U3CU3Ec__DisplayClass21_0_tD13CB1F6A584B52EE6BB08DF9357A32458E13157* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* G_B2_0 = NULL;
	Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* G_B1_0 = NULL;
	{
		// callBack?.Invoke(soundPlayer);
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_0 = __this->___callBack_0;
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000c;
		}
	}
	{
		goto IL_0017;
	}

IL_000c:
	{
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_2 = __this->___soundPlayer_1;
		NullCheck(G_B2_0);
		Action_1_Invoke_m0983EE053D0C9620014033A591393EE7C83C9A97_inline(G_B2_0, L_2, NULL);
	}

IL_0017:
	{
		// AudioManager.Instance.RemoveSoundPlayerFromPool(soundPlayer);
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_3;
		L_3 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_4 = __this->___soundPlayer_1;
		NullCheck(L_3);
		AudioManager_RemoveSoundPlayerFromPool_m144B7764E7D26C9EF81AE0F0C8E9BBAB74297E0E(L_3, L_4, NULL);
		// SoundFinish(soundName);
		String_t* L_5 = __this->___soundName_2;
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKit_SoundFinish_m5A8751B115239ABA1523914682619A52B948FD2D(L_5, NULL);
		// });
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
// System.Void QFramework.AudioKit/<>c::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__cctor_mBB5DBE49B0060494E734F2B87F90971AC08EC318 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC* L_0 = (U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC*)il2cpp_codegen_object_new(U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__ctor_mEA1449FC60866C3F2C7CBA531E071E4E49F48865(L_0, NULL);
		((U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_il2cpp_TypeInfo_var))->___U3CU3E9_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC_il2cpp_TypeInfo_var))->___U3CU3E9_0), (void*)L_0);
		return;
	}
}
// System.Void QFramework.AudioKit/<>c::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__ctor_mEA1449FC60866C3F2C7CBA531E071E4E49F48865 (U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void QFramework.AudioKit/<>c::<StopAllSound>b__22_0(QFramework.AudioPlayer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec_U3CStopAllSoundU3Eb__22_0_m90BB3B9C70D28CFE509799F933282F40E10125BB (U3CU3Ec_t9BE76F480A057492E2CF6B0C72234FDD3EFCD3BC* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_player, const RuntimeMethod* method) 
{
	{
		// AudioManager.Instance.ForEachAllSound(player => player.Stop());
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_0 = ___0_player;
		NullCheck(L_0);
		AudioPlayer_Stop_m76865190573766D55E4533B8AEA5AE7F8834E96B(L_0, NULL);
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
// System.Void QFramework.AudioKit/<>c__DisplayClass25_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass25_0__ctor_m74B5819BBCF04D3324DF32EAABD3064AAC6A16C2 (U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void QFramework.AudioKit/<>c__DisplayClass25_0::<PlaySound>b__0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass25_0_U3CPlaySoundU3Eb__0_m8D85FB5C48586757CCB3D6BD15B129DC3F0038D6 (U3CU3Ec__DisplayClass25_0_t5F5900B9041CC8AFAF52E43F9CA7AB75D59E3C51* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* G_B2_0 = NULL;
	Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* G_B1_0 = NULL;
	{
		// callBack?.Invoke(soundPlayer);
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_0 = __this->___callBack_0;
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000c;
		}
	}
	{
		goto IL_0017;
	}

IL_000c:
	{
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_2 = __this->___soundPlayer_1;
		NullCheck(G_B2_0);
		Action_1_Invoke_m0983EE053D0C9620014033A591393EE7C83C9A97_inline(G_B2_0, L_2, NULL);
	}

IL_0017:
	{
		// AudioManager.Instance.RemoveSoundPlayerFromPool(soundPlayer);
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_3;
		L_3 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_4 = __this->___soundPlayer_1;
		NullCheck(L_3);
		AudioManager_RemoveSoundPlayerFromPool_m144B7764E7D26C9EF81AE0F0C8E9BBAB74297E0E(L_3, L_4, NULL);
		// });
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
// System.Void QFramework.AudioKitConfig::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitConfig__ctor_m4209BF93AC62060B70335750DE6D083C7699565E (AudioKitConfig_t0518724CE4F14A672627665BE74D533751686A54* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DefaultAudioLoaderPool_t83CA69E29CA127237BADCBB67697143A0ED7D53A_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public IAudioLoaderPool AudioLoaderPool = new DefaultAudioLoaderPool();
		DefaultAudioLoaderPool_t83CA69E29CA127237BADCBB67697143A0ED7D53A* L_0 = (DefaultAudioLoaderPool_t83CA69E29CA127237BADCBB67697143A0ED7D53A*)il2cpp_codegen_object_new(DefaultAudioLoaderPool_t83CA69E29CA127237BADCBB67697143A0ED7D53A_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		DefaultAudioLoaderPool__ctor_m23B68C1F6795C1125F1229442C9E30486CC27751(L_0, NULL);
		__this->___AudioLoaderPool_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___AudioLoaderPool_0), (void*)L_0);
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
// System.Void QFramework.AudioSearchKeys::OnRecycled()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSearchKeys_OnRecycled_mB1D543DB4408DA8A56F561E101589E5D674EF926 (AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* __this, const RuntimeMethod* method) 
{
	{
		// AssetBundleName = null;
		__this->___AssetBundleName_0 = (String_t*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___AssetBundleName_0), (void*)(String_t*)NULL);
		// AssetName = null;
		__this->___AssetName_1 = (String_t*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___AssetName_1), (void*)(String_t*)NULL);
		// }
		return;
	}
}
// System.Boolean QFramework.AudioSearchKeys::get_IsRecycled()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioSearchKeys_get_IsRecycled_m53CEA8CEBFF332010B6FD27096BF333EFD982D51 (AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsRecycled { get; set; }
		bool L_0 = __this->___U3CIsRecycledU3Ek__BackingField_2;
		return L_0;
	}
}
// System.Void QFramework.AudioSearchKeys::set_IsRecycled(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSearchKeys_set_IsRecycled_m23A244B2E8EBBD92CACCCC55B0DB5FE84641F49E (AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		// public bool IsRecycled { get; set; }
		bool L_0 = ___0_value;
		__this->___U3CIsRecycledU3Ek__BackingField_2 = L_0;
		return;
	}
}
// System.String QFramework.AudioSearchKeys::ToString()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AudioSearchKeys_ToString_mF4B25185B220FC2DADAB830FCEEA1A7732455748 (AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralAD7DC71B936C20931E35C4FE34CF4D1D3056E0A5);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralC10CC291369A31E4DE72DE6E97E5E2EF7339A12B);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return
		//     $"AudioSearchKeys AssetName:{AssetName} AssetBundleName:{AssetBundleName}";
		String_t* L_0 = __this->___AssetName_1;
		String_t* L_1 = __this->___AssetBundleName_0;
		String_t* L_2;
		L_2 = String_Concat_m093934F71A9B351911EE46311674ED463B180006(_stringLiteralC10CC291369A31E4DE72DE6E97E5E2EF7339A12B, L_0, _stringLiteralAD7DC71B936C20931E35C4FE34CF4D1D3056E0A5, L_1, NULL);
		return L_2;
	}
}
// QFramework.AudioSearchKeys QFramework.AudioSearchKeys::Allocate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* AudioSearchKeys_Allocate_mCE7D3F47B99F8F3EDF78A16B3031F9D93EC906D1 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SafeObjectPool_1_get_Instance_m10308F90A4BD9A0D5A63EC07A735B36D00C075ED_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return SafeObjectPool<AudioSearchKeys>.Instance.Allocate();
		SafeObjectPool_1_t7D55BDF522ED56ECFF3CBDB6115923D35053E535* L_0;
		L_0 = SafeObjectPool_1_get_Instance_m10308F90A4BD9A0D5A63EC07A735B36D00C075ED(SafeObjectPool_1_get_Instance_m10308F90A4BD9A0D5A63EC07A735B36D00C075ED_RuntimeMethod_var);
		NullCheck(L_0);
		AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* L_1;
		L_1 = VirtualFuncInvoker0< AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* >::Invoke(6 /* T QFramework.Pool`1<QFramework.AudioSearchKeys>::Allocate() */, L_0);
		return L_1;
	}
}
// System.Void QFramework.AudioSearchKeys::Recycle2Cache()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSearchKeys_Recycle2Cache_mD5F065BE8C2A1E01CFF642CF29FE8F969F0835E9 (AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SafeObjectPool_1_get_Instance_m10308F90A4BD9A0D5A63EC07A735B36D00C075ED_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// SafeObjectPool<AudioSearchKeys>.Instance.Recycle(this);
		SafeObjectPool_1_t7D55BDF522ED56ECFF3CBDB6115923D35053E535* L_0;
		L_0 = SafeObjectPool_1_get_Instance_m10308F90A4BD9A0D5A63EC07A735B36D00C075ED(SafeObjectPool_1_get_Instance_m10308F90A4BD9A0D5A63EC07A735B36D00C075ED_RuntimeMethod_var);
		NullCheck(L_0);
		bool L_1;
		L_1 = VirtualFuncInvoker1< bool, AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* >::Invoke(7 /* System.Boolean QFramework.Pool`1<QFramework.AudioSearchKeys>::Recycle(T) */, L_0, __this);
		// }
		return;
	}
}
// System.Void QFramework.AudioSearchKeys::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioSearchKeys__ctor_mB441208F43EF0F2A56A27667A76486AB74B4A45F (AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* __this, const RuntimeMethod* method) 
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
// QFramework.IAudioLoader QFramework.AbstractAudioLoaderPool::AllocateLoader()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* AbstractAudioLoaderPool_AllocateLoader_m5D2C2D24F5CCDB72A6CDB0BE11BC96EEE4D539D8 (AbstractAudioLoaderPool_tA9514ECF9B0920914A41DA1E94F9C7914109136A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stack_1_Pop_m7109081E7676FB662FEB90062F1A6C4A288E596A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stack_1_get_Count_mE28FA86A5FE81403FA112BE1E3D928DAFCD15F0E_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return mPool.Count > 0 ? mPool.Pop() : CreateLoader();
		Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5* L_0 = __this->___mPool_0;
		NullCheck(L_0);
		int32_t L_1;
		L_1 = Stack_1_get_Count_mE28FA86A5FE81403FA112BE1E3D928DAFCD15F0E_inline(L_0, Stack_1_get_Count_mE28FA86A5FE81403FA112BE1E3D928DAFCD15F0E_RuntimeMethod_var);
		if ((((int32_t)L_1) > ((int32_t)0)))
		{
			goto IL_0015;
		}
	}
	{
		RuntimeObject* L_2;
		L_2 = VirtualFuncInvoker0< RuntimeObject* >::Invoke(6 /* QFramework.IAudioLoader QFramework.AbstractAudioLoaderPool::CreateLoader() */, __this);
		return L_2;
	}

IL_0015:
	{
		Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5* L_3 = __this->___mPool_0;
		NullCheck(L_3);
		RuntimeObject* L_4;
		L_4 = Stack_1_Pop_m7109081E7676FB662FEB90062F1A6C4A288E596A(L_3, Stack_1_Pop_m7109081E7676FB662FEB90062F1A6C4A288E596A_RuntimeMethod_var);
		return L_4;
	}
}
// System.Void QFramework.AbstractAudioLoaderPool::RecycleLoader(QFramework.IAudioLoader)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AbstractAudioLoaderPool_RecycleLoader_m0ED8AB1697CEF5E266399BD3A9F94FF34C2BBA6D (AbstractAudioLoaderPool_tA9514ECF9B0920914A41DA1E94F9C7914109136A* __this, RuntimeObject* ___0_loader, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stack_1_Push_m837A07DFF4ED3EC7B750D3EF3E9DFF8E4C713DD0_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// mPool.Push(loader);
		Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5* L_0 = __this->___mPool_0;
		RuntimeObject* L_1 = ___0_loader;
		NullCheck(L_0);
		Stack_1_Push_m837A07DFF4ED3EC7B750D3EF3E9DFF8E4C713DD0(L_0, L_1, Stack_1_Push_m837A07DFF4ED3EC7B750D3EF3E9DFF8E4C713DD0_RuntimeMethod_var);
		// }
		return;
	}
}
// System.Void QFramework.AbstractAudioLoaderPool::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AbstractAudioLoaderPool__ctor_m7A9A7C0BE215C31A8C21932F912A67EE678D7C02 (AbstractAudioLoaderPool_tA9514ECF9B0920914A41DA1E94F9C7914109136A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stack_1__ctor_mDA3004D09A08CCD4F349E97A1A95B76B9D5D41F6_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// private Stack<IAudioLoader> mPool = new Stack<IAudioLoader>(16);
		Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5* L_0 = (Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5*)il2cpp_codegen_object_new(Stack_1_t23DB2FF56446086FE36118E5B3E7E1414359A3C5_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Stack_1__ctor_mDA3004D09A08CCD4F349E97A1A95B76B9D5D41F6(L_0, ((int32_t)16), Stack_1__ctor_mDA3004D09A08CCD4F349E97A1A95B76B9D5D41F6_RuntimeMethod_var);
		__this->___mPool_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mPool_0), (void*)L_0);
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
// QFramework.IAudioLoader QFramework.DefaultAudioLoaderPool::CreateLoader()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* DefaultAudioLoaderPool_CreateLoader_mF50248BB0B9F9A5813DB6757266C3BD12D63E285 (DefaultAudioLoaderPool_t83CA69E29CA127237BADCBB67697143A0ED7D53A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&DefaultAudioLoader_tA58E6CBC81292CF195A2FED8AF13ABF1473CF43F_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return new DefaultAudioLoader();
		DefaultAudioLoader_tA58E6CBC81292CF195A2FED8AF13ABF1473CF43F* L_0 = (DefaultAudioLoader_tA58E6CBC81292CF195A2FED8AF13ABF1473CF43F*)il2cpp_codegen_object_new(DefaultAudioLoader_tA58E6CBC81292CF195A2FED8AF13ABF1473CF43F_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		DefaultAudioLoader__ctor_mEE4834113F8F8C71ECE19AC27F895E2F0C85B95F(L_0, NULL);
		return L_0;
	}
}
// System.Void QFramework.DefaultAudioLoaderPool::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DefaultAudioLoaderPool__ctor_m23B68C1F6795C1125F1229442C9E30486CC27751 (DefaultAudioLoaderPool_t83CA69E29CA127237BADCBB67697143A0ED7D53A* __this, const RuntimeMethod* method) 
{
	{
		AbstractAudioLoaderPool__ctor_m7A9A7C0BE215C31A8C21932F912A67EE678D7C02(__this, NULL);
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
// UnityEngine.AudioClip QFramework.DefaultAudioLoader::get_Clip()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* DefaultAudioLoader_get_Clip_m70F5FC1C8DB49BA87FE142E04B5C3D07BB964024 (DefaultAudioLoader_tA58E6CBC81292CF195A2FED8AF13ABF1473CF43F* __this, const RuntimeMethod* method) 
{
	{
		// public AudioClip Clip => mClip;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_0 = __this->___mClip_0;
		return L_0;
	}
}
// UnityEngine.AudioClip QFramework.DefaultAudioLoader::LoadClip(QFramework.AudioSearchKeys)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* DefaultAudioLoader_LoadClip_mC2FB48C1793B873E7B5C21E3261EC759AE5EBEFF (DefaultAudioLoader_tA58E6CBC81292CF195A2FED8AF13ABF1473CF43F* __this, AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* ___0_panelSearchKeys, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Resources_Load_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m8D55846FD24C1D133D4EA744BE1E73E054B93A78_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// mClip = Resources.Load<AudioClip>(panelSearchKeys.AssetName);
		AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* L_0 = ___0_panelSearchKeys;
		NullCheck(L_0);
		String_t* L_1 = L_0->___AssetName_1;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_2;
		L_2 = Resources_Load_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m8D55846FD24C1D133D4EA744BE1E73E054B93A78(L_1, Resources_Load_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m8D55846FD24C1D133D4EA744BE1E73E054B93A78_RuntimeMethod_var);
		__this->___mClip_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mClip_0), (void*)L_2);
		// return mClip;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_3 = __this->___mClip_0;
		return L_3;
	}
}
// System.Void QFramework.DefaultAudioLoader::LoadClipAsync(QFramework.AudioSearchKeys,System.Action`2<System.Boolean,UnityEngine.AudioClip>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DefaultAudioLoader_LoadClipAsync_mE5FC5A7594B915FC9B294DE1C454DD8F3CE26D4C (DefaultAudioLoader_tA58E6CBC81292CF195A2FED8AF13ABF1473CF43F* __this, AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* ___0_audioSearchKeys, Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD* ___1_onLoad, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Resources_LoadAsync_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m0D27E9CB67015D736F22E3AFAB5E6108ABA9A740_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass4_0_U3CLoadClipAsyncU3Eb__0_mF979D841FD744ADE23D11771DC3F2DA1A2799E24_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B* V_0 = NULL;
	{
		U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B* L_0 = (U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass4_0__ctor_mD7A7A1A6DB4B58B568D36E7B3BE49C1A6ED05908(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B* L_1 = V_0;
		Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD* L_2 = ___1_onLoad;
		NullCheck(L_1);
		L_1->___onLoad_1 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___onLoad_1), (void*)L_2);
		// var resourceRequest = Resources.LoadAsync<AudioClip>(audioSearchKeys.AssetName);
		U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B* L_3 = V_0;
		AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* L_4 = ___0_audioSearchKeys;
		NullCheck(L_4);
		String_t* L_5 = L_4->___AssetName_1;
		ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868* L_6;
		L_6 = Resources_LoadAsync_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m0D27E9CB67015D736F22E3AFAB5E6108ABA9A740(L_5, Resources_LoadAsync_TisAudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_m0D27E9CB67015D736F22E3AFAB5E6108ABA9A740_RuntimeMethod_var);
		NullCheck(L_3);
		L_3->___resourceRequest_0 = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&L_3->___resourceRequest_0), (void*)L_6);
		// resourceRequest.completed += operation =>
		// {
		//     var clip = resourceRequest.asset as AudioClip;
		//     onLoad(clip, clip);
		// };
		U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B* L_7 = V_0;
		NullCheck(L_7);
		ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868* L_8 = L_7->___resourceRequest_0;
		U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B* L_9 = V_0;
		Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB* L_10 = (Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB*)il2cpp_codegen_object_new(Action_1_tE8693FF0E67CDBA52BAFB211BFF1844D076ABAFB_il2cpp_TypeInfo_var);
		NullCheck(L_10);
		Action_1__ctor_m33ABB7530487276910BEFB499A97D33FB2E06D7D(L_10, L_9, (intptr_t)((void*)U3CU3Ec__DisplayClass4_0_U3CLoadClipAsyncU3Eb__0_mF979D841FD744ADE23D11771DC3F2DA1A2799E24_RuntimeMethod_var), NULL);
		NullCheck(L_8);
		AsyncOperation_add_completed_mD6F21BA8127D6D4B7ABDEFAA995A7A347A20A793(L_8, L_10, NULL);
		// }
		return;
	}
}
// System.Void QFramework.DefaultAudioLoader::Unload()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DefaultAudioLoader_Unload_m13B0AB2316DACE7FDC47C55E103A3E06712803D1 (DefaultAudioLoader_tA58E6CBC81292CF195A2FED8AF13ABF1473CF43F* __this, const RuntimeMethod* method) 
{
	{
		// Resources.UnloadAsset(mClip);
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_0 = __this->___mClip_0;
		Resources_UnloadAsset_m4828D1393356C052061C66403B1437F7D7E21908(L_0, NULL);
		// }
		return;
	}
}
// System.Void QFramework.DefaultAudioLoader::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void DefaultAudioLoader__ctor_mEE4834113F8F8C71ECE19AC27F895E2F0C85B95F (DefaultAudioLoader_tA58E6CBC81292CF195A2FED8AF13ABF1473CF43F* __this, const RuntimeMethod* method) 
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
// System.Void QFramework.DefaultAudioLoader/<>c__DisplayClass4_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass4_0__ctor_mD7A7A1A6DB4B58B568D36E7B3BE49C1A6ED05908 (U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void QFramework.DefaultAudioLoader/<>c__DisplayClass4_0::<LoadClipAsync>b__0(UnityEngine.AsyncOperation)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass4_0_U3CLoadClipAsyncU3Eb__0_mF979D841FD744ADE23D11771DC3F2DA1A2799E24 (U3CU3Ec__DisplayClass4_0_t3D3232B03C5502D60D3EB82462F807DFA636912B* __this, AsyncOperation_tD2789250E4B098DEDA92B366A577E500A92D2D3C* ___0_operation, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* V_0 = NULL;
	{
		// var clip = resourceRequest.asset as AudioClip;
		ResourceRequest_tE6953FBA45EAAEFE866C635B9E7852044E62D868* L_0 = __this->___resourceRequest_0;
		NullCheck(L_0);
		Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C* L_1;
		L_1 = ResourceRequest_get_asset_m740C394902843D080795A49372A2AB3CE9705087(L_0, NULL);
		V_0 = ((AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*)IsInstSealed((RuntimeObject*)L_1, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20_il2cpp_TypeInfo_var));
		// onLoad(clip, clip);
		Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD* L_2 = __this->___onLoad_1;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_3 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_4;
		L_4 = Object_op_Implicit_m93896EF7D68FA113C42D3FE2BC6F661FC7EF514A(L_3, NULL);
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_5 = V_0;
		NullCheck(L_2);
		Action_2_Invoke_mF9B25C8213BEBD9D83124082D0CDB0859A9F016D_inline(L_2, L_4, L_5, NULL);
		// };
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
// System.Void QFramework.AudioKitSettings::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitSettings__ctor_m0D57B7E51C1244C01164A711F52CA0FB77DD88F1 (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKitSettings_U3C_ctorU3Eb__6_0_m0EA65EB385532EB11451EC4BF97849B38CE0FFE4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKitSettings_U3C_ctorU3Eb__6_1_m193FF57433E10C590810CCC2C728EBCE870AFA50_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CustomProperty_1__ctor_mF71DC5C64029750FFE0EB414A8D4AA9765719458_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral4916DA5F3D35D35493CBB5556B638FE8093E5A6D);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral728B387DF77B756CAE5319FD715930757F9B615F);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral9BA7B8AFF7E32B71E5A1503CF07A3758837776ED);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralC88326BF66F1656CEB1B68D4540896C71001AE2B);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralCF47A8518514248D2A2EB59BB80CB4D2602DA048);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralF0B34B7D0C316DF89CF323EDF95AF14BEF34823A);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public AudioKitSettings()
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		// IsSoundOn = new PlayerPrefsBooleanProperty(KEY_AUDIO_MANAGER_SOUND_ON, true);
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0 = (PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3*)il2cpp_codegen_object_new(PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		PlayerPrefsBooleanProperty__ctor_m4109A6099AC15D8224C30BD7FE6F800967C2D541(L_0, _stringLiteralCF47A8518514248D2A2EB59BB80CB4D2602DA048, (bool)1, NULL);
		AudioKitSettings_set_IsSoundOn_m3B8938CB24A93E9864A4152064F6A7E66B12FD22_inline(__this, L_0, NULL);
		// IsMusicOn = new PlayerPrefsBooleanProperty(KEY_AUDIO_MANAGER_MUSIC_ON, true);
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_1 = (PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3*)il2cpp_codegen_object_new(PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		PlayerPrefsBooleanProperty__ctor_m4109A6099AC15D8224C30BD7FE6F800967C2D541(L_1, _stringLiteral4916DA5F3D35D35493CBB5556B638FE8093E5A6D, (bool)1, NULL);
		AudioKitSettings_set_IsMusicOn_mB7BB81A6F1F2CF2A58C28A0FF40783B6603D48CB_inline(__this, L_1, NULL);
		// IsVoiceOn = new PlayerPrefsBooleanProperty(KEY_AUDIO_MANAGER_VOICE_ON, true);
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_2 = (PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3*)il2cpp_codegen_object_new(PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3_il2cpp_TypeInfo_var);
		NullCheck(L_2);
		PlayerPrefsBooleanProperty__ctor_m4109A6099AC15D8224C30BD7FE6F800967C2D541(L_2, _stringLiteralF0B34B7D0C316DF89CF323EDF95AF14BEF34823A, (bool)1, NULL);
		AudioKitSettings_set_IsVoiceOn_m22A05BA843579B70656A6B4B7C95163882DD30C0_inline(__this, L_2, NULL);
		// IsOn = new CustomProperty<bool>(
		//     () => IsSoundOn.Value && IsMusicOn.Value && IsVoiceOn.Value,
		//     isOn =>
		//     {
		//         Debug.Log(isOn);
		//         IsSoundOn.Value = isOn;
		//         IsMusicOn.Value = isOn;
		//         IsVoiceOn.Value = isOn;
		//     }
		// );
		Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457* L_3 = (Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457*)il2cpp_codegen_object_new(Func_1_t2BE7F58348C9CC544A8973B3A9E55541DE43C457_il2cpp_TypeInfo_var);
		NullCheck(L_3);
		Func_1__ctor_mDFFAE9C73346372438B5B04C4558AC42F1A3DA22(L_3, __this, (intptr_t)((void*)AudioKitSettings_U3C_ctorU3Eb__6_0_m0EA65EB385532EB11451EC4BF97849B38CE0FFE4_RuntimeMethod_var), NULL);
		Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* L_4 = (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C*)il2cpp_codegen_object_new(Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		Action_1__ctor_mA8C3AC97D1F076EA5D1D0C10CEE6BD3E94711501(L_4, __this, (intptr_t)((void*)AudioKitSettings_U3C_ctorU3Eb__6_1_m193FF57433E10C590810CCC2C728EBCE870AFA50_RuntimeMethod_var), NULL);
		CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A* L_5 = (CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A*)il2cpp_codegen_object_new(CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A_il2cpp_TypeInfo_var);
		NullCheck(L_5);
		CustomProperty_1__ctor_mF71DC5C64029750FFE0EB414A8D4AA9765719458(L_5, L_3, L_4, CustomProperty_1__ctor_mF71DC5C64029750FFE0EB414A8D4AA9765719458_RuntimeMethod_var);
		AudioKitSettings_set_IsOn_m4E88FB457E5A9FA95CAB7591BD86C9ECC5A7742E_inline(__this, L_5, NULL);
		// SoundVolume = new PlayerPrefsFloatProperty(KEY_AUDIO_MANAGER_SOUND_VOLUME, 1.0f);
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_6 = (PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793*)il2cpp_codegen_object_new(PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793_il2cpp_TypeInfo_var);
		NullCheck(L_6);
		PlayerPrefsFloatProperty__ctor_mC05DD0E1587A27B45A0CFB2E97B3E288A38CD7E0(L_6, _stringLiteralC88326BF66F1656CEB1B68D4540896C71001AE2B, (1.0f), NULL);
		AudioKitSettings_set_SoundVolume_mAB196708DD249054162A3B89B53CD1C5564A9954_inline(__this, L_6, NULL);
		// MusicVolume = new PlayerPrefsFloatProperty(KEY_AUDIO_MANAGER_MUSIC_VOLUME, 1.0f);
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_7 = (PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793*)il2cpp_codegen_object_new(PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793_il2cpp_TypeInfo_var);
		NullCheck(L_7);
		PlayerPrefsFloatProperty__ctor_mC05DD0E1587A27B45A0CFB2E97B3E288A38CD7E0(L_7, _stringLiteral728B387DF77B756CAE5319FD715930757F9B615F, (1.0f), NULL);
		AudioKitSettings_set_MusicVolume_m31F575D40B044BC109FE19B8AEF8575C8995A55B_inline(__this, L_7, NULL);
		// VoiceVolume = new PlayerPrefsFloatProperty(KEY_AUDIO_MANAGER_VOICE_VOLUME, 1.0f);
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_8 = (PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793*)il2cpp_codegen_object_new(PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793_il2cpp_TypeInfo_var);
		NullCheck(L_8);
		PlayerPrefsFloatProperty__ctor_mC05DD0E1587A27B45A0CFB2E97B3E288A38CD7E0(L_8, _stringLiteral9BA7B8AFF7E32B71E5A1503CF07A3758837776ED, (1.0f), NULL);
		AudioKitSettings_set_VoiceVolume_m9477C782964C3652242B54F32C82906A44350D3A_inline(__this, L_8, NULL);
		// }
		return;
	}
}
// QFramework.PlayerPrefsBooleanProperty QFramework.AudioKitSettings::get_IsSoundOn()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* AudioKitSettings_get_IsSoundOn_m16E5D82CD5CE9BDAA868820A75FA970239276A85 (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsBooleanProperty IsSoundOn { get; private set; }
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0 = __this->___U3CIsSoundOnU3Ek__BackingField_6;
		return L_0;
	}
}
// System.Void QFramework.AudioKitSettings::set_IsSoundOn(QFramework.PlayerPrefsBooleanProperty)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitSettings_set_IsSoundOn_m3B8938CB24A93E9864A4152064F6A7E66B12FD22 (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* ___0_value, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsBooleanProperty IsSoundOn { get; private set; }
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0 = ___0_value;
		__this->___U3CIsSoundOnU3Ek__BackingField_6 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CIsSoundOnU3Ek__BackingField_6), (void*)L_0);
		return;
	}
}
// QFramework.PlayerPrefsBooleanProperty QFramework.AudioKitSettings::get_IsMusicOn()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* AudioKitSettings_get_IsMusicOn_mB0034F4E51AEAC7FF364B848A6A42CD30E1CBB89 (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsBooleanProperty IsMusicOn { get; private set; }
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0 = __this->___U3CIsMusicOnU3Ek__BackingField_7;
		return L_0;
	}
}
// System.Void QFramework.AudioKitSettings::set_IsMusicOn(QFramework.PlayerPrefsBooleanProperty)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitSettings_set_IsMusicOn_mB7BB81A6F1F2CF2A58C28A0FF40783B6603D48CB (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* ___0_value, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsBooleanProperty IsMusicOn { get; private set; }
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0 = ___0_value;
		__this->___U3CIsMusicOnU3Ek__BackingField_7 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CIsMusicOnU3Ek__BackingField_7), (void*)L_0);
		return;
	}
}
// QFramework.PlayerPrefsBooleanProperty QFramework.AudioKitSettings::get_IsVoiceOn()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* AudioKitSettings_get_IsVoiceOn_m40198BDFF0BAC22A00FF9A7D42A90D9AF6BDF937 (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsBooleanProperty IsVoiceOn { get; private set; }
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0 = __this->___U3CIsVoiceOnU3Ek__BackingField_8;
		return L_0;
	}
}
// System.Void QFramework.AudioKitSettings::set_IsVoiceOn(QFramework.PlayerPrefsBooleanProperty)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitSettings_set_IsVoiceOn_m22A05BA843579B70656A6B4B7C95163882DD30C0 (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* ___0_value, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsBooleanProperty IsVoiceOn { get; private set; }
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0 = ___0_value;
		__this->___U3CIsVoiceOnU3Ek__BackingField_8 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CIsVoiceOnU3Ek__BackingField_8), (void*)L_0);
		return;
	}
}
// QFramework.PlayerPrefsFloatProperty QFramework.AudioKitSettings::get_SoundVolume()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* AudioKitSettings_get_SoundVolume_mD628B2C76B4EE61E1BF923E41126A63B0F3B897A (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsFloatProperty SoundVolume { get; private set; }
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_0 = __this->___U3CSoundVolumeU3Ek__BackingField_9;
		return L_0;
	}
}
// System.Void QFramework.AudioKitSettings::set_SoundVolume(QFramework.PlayerPrefsFloatProperty)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitSettings_set_SoundVolume_mAB196708DD249054162A3B89B53CD1C5564A9954 (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* ___0_value, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsFloatProperty SoundVolume { get; private set; }
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_0 = ___0_value;
		__this->___U3CSoundVolumeU3Ek__BackingField_9 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CSoundVolumeU3Ek__BackingField_9), (void*)L_0);
		return;
	}
}
// QFramework.PlayerPrefsFloatProperty QFramework.AudioKitSettings::get_MusicVolume()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* AudioKitSettings_get_MusicVolume_mC12F412255E5C5D80823C6B401EA3B76CEA9EC84 (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsFloatProperty MusicVolume { get; private set; }
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_0 = __this->___U3CMusicVolumeU3Ek__BackingField_10;
		return L_0;
	}
}
// System.Void QFramework.AudioKitSettings::set_MusicVolume(QFramework.PlayerPrefsFloatProperty)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitSettings_set_MusicVolume_m31F575D40B044BC109FE19B8AEF8575C8995A55B (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* ___0_value, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsFloatProperty MusicVolume { get; private set; }
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_0 = ___0_value;
		__this->___U3CMusicVolumeU3Ek__BackingField_10 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CMusicVolumeU3Ek__BackingField_10), (void*)L_0);
		return;
	}
}
// QFramework.PlayerPrefsFloatProperty QFramework.AudioKitSettings::get_VoiceVolume()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* AudioKitSettings_get_VoiceVolume_m67A62365F356F9A35A31A385452EDBCBFB99F3C1 (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsFloatProperty VoiceVolume { get; private set; }
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_0 = __this->___U3CVoiceVolumeU3Ek__BackingField_11;
		return L_0;
	}
}
// System.Void QFramework.AudioKitSettings::set_VoiceVolume(QFramework.PlayerPrefsFloatProperty)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitSettings_set_VoiceVolume_m9477C782964C3652242B54F32C82906A44350D3A (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* ___0_value, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsFloatProperty VoiceVolume { get; private set; }
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_0 = ___0_value;
		__this->___U3CVoiceVolumeU3Ek__BackingField_11 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CVoiceVolumeU3Ek__BackingField_11), (void*)L_0);
		return;
	}
}
// QFramework.CustomProperty`1<System.Boolean> QFramework.AudioKitSettings::get_IsOn()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A* AudioKitSettings_get_IsOn_mFC2443A5B3703F206ACAB83E7BC1900534DA13FB (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	{
		// public CustomProperty<bool> IsOn { get; private set; }
		CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A* L_0 = __this->___U3CIsOnU3Ek__BackingField_12;
		return L_0;
	}
}
// System.Void QFramework.AudioKitSettings::set_IsOn(QFramework.CustomProperty`1<System.Boolean>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitSettings_set_IsOn_m4E88FB457E5A9FA95CAB7591BD86C9ECC5A7742E (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A* ___0_value, const RuntimeMethod* method) 
{
	{
		// public CustomProperty<bool> IsOn { get; private set; }
		CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A* L_0 = ___0_value;
		__this->___U3CIsOnU3Ek__BackingField_12 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CIsOnU3Ek__BackingField_12), (void*)L_0);
		return;
	}
}
// System.Boolean QFramework.AudioKitSettings::<.ctor>b__6_0()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioKitSettings_U3C_ctorU3Eb__6_0_m0EA65EB385532EB11451EC4BF97849B38CE0FFE4 (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// () => IsSoundOn.Value && IsMusicOn.Value && IsVoiceOn.Value,
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0;
		L_0 = AudioKitSettings_get_IsSoundOn_m16E5D82CD5CE9BDAA868820A75FA970239276A85_inline(__this, NULL);
		NullCheck(L_0);
		bool L_1;
		L_1 = BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18(L_0, BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_RuntimeMethod_var);
		if (!L_1)
		{
			goto IL_0026;
		}
	}
	{
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_2;
		L_2 = AudioKitSettings_get_IsMusicOn_mB0034F4E51AEAC7FF364B848A6A42CD30E1CBB89_inline(__this, NULL);
		NullCheck(L_2);
		bool L_3;
		L_3 = BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18(L_2, BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_RuntimeMethod_var);
		if (!L_3)
		{
			goto IL_0026;
		}
	}
	{
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_4;
		L_4 = AudioKitSettings_get_IsVoiceOn_m40198BDFF0BAC22A00FF9A7D42A90D9AF6BDF937_inline(__this, NULL);
		NullCheck(L_4);
		bool L_5;
		L_5 = BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18(L_4, BindableProperty_1_get_Value_m242A33D35FCC2FB1168CD57211B73E7FB0FA7D18_RuntimeMethod_var);
		return L_5;
	}

IL_0026:
	{
		return (bool)0;
	}
}
// System.Void QFramework.AudioKitSettings::<.ctor>b__6_1(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitSettings_U3C_ctorU3Eb__6_1_m193FF57433E10C590810CCC2C728EBCE870AFA50 (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, bool ___0_isOn, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BindableProperty_1_set_Value_m4F9D20068AFC366BBC972B46D6A54C5FC8BDE651_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Debug.Log(isOn);
		bool L_0 = ___0_isOn;
		bool L_1 = L_0;
		RuntimeObject* L_2 = Box(Boolean_t09A6377A54BE2F9E6985A8149F19234FD7DDFE22_il2cpp_TypeInfo_var, &L_1);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(L_2, NULL);
		// IsSoundOn.Value = isOn;
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_3;
		L_3 = AudioKitSettings_get_IsSoundOn_m16E5D82CD5CE9BDAA868820A75FA970239276A85_inline(__this, NULL);
		bool L_4 = ___0_isOn;
		NullCheck(L_3);
		BindableProperty_1_set_Value_m4F9D20068AFC366BBC972B46D6A54C5FC8BDE651(L_3, L_4, BindableProperty_1_set_Value_m4F9D20068AFC366BBC972B46D6A54C5FC8BDE651_RuntimeMethod_var);
		// IsMusicOn.Value = isOn;
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_5;
		L_5 = AudioKitSettings_get_IsMusicOn_mB0034F4E51AEAC7FF364B848A6A42CD30E1CBB89_inline(__this, NULL);
		bool L_6 = ___0_isOn;
		NullCheck(L_5);
		BindableProperty_1_set_Value_m4F9D20068AFC366BBC972B46D6A54C5FC8BDE651(L_5, L_6, BindableProperty_1_set_Value_m4F9D20068AFC366BBC972B46D6A54C5FC8BDE651_RuntimeMethod_var);
		// IsVoiceOn.Value = isOn;
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_7;
		L_7 = AudioKitSettings_get_IsVoiceOn_m40198BDFF0BAC22A00FF9A7D42A90D9AF6BDF937_inline(__this, NULL);
		bool L_8 = ___0_isOn;
		NullCheck(L_7);
		BindableProperty_1_set_Value_m4F9D20068AFC366BBC972B46D6A54C5FC8BDE651(L_7, L_8, BindableProperty_1_set_Value_m4F9D20068AFC366BBC972B46D6A54C5FC8BDE651_RuntimeMethod_var);
		// }
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
// QFramework.AudioPlayer QFramework.AudioManager::get_MusicPlayer()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioManager_get_MusicPlayer_m8046BC707549A3B916F8C074D12CB7E6C79E27BA (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) 
{
	{
		// public AudioPlayer MusicPlayer { get; private set; }
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_0 = __this->___U3CMusicPlayerU3Ek__BackingField_4;
		return L_0;
	}
}
// System.Void QFramework.AudioManager::set_MusicPlayer(QFramework.AudioPlayer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_set_MusicPlayer_m19CF9C6DCBACE60B3B532458F590BE8B4A6B9ACF (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_value, const RuntimeMethod* method) 
{
	{
		// public AudioPlayer MusicPlayer { get; private set; }
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_0 = ___0_value;
		__this->___U3CMusicPlayerU3Ek__BackingField_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CMusicPlayerU3Ek__BackingField_4), (void*)L_0);
		return;
	}
}
// QFramework.AudioPlayer QFramework.AudioManager::get_VoicePlayer()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioManager_get_VoicePlayer_m2FFBB17585A0BEEB38C67CF63A693A9F38C87D3D (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) 
{
	{
		// public AudioPlayer VoicePlayer { get; private set; }
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_0 = __this->___U3CVoicePlayerU3Ek__BackingField_5;
		return L_0;
	}
}
// System.Void QFramework.AudioManager::set_VoicePlayer(QFramework.AudioPlayer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_set_VoicePlayer_mE9427043D0CD26480779BA21020D4C22FD7E40BC (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_value, const RuntimeMethod* method) 
{
	{
		// public AudioPlayer VoicePlayer { get; private set; }
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_0 = ___0_value;
		__this->___U3CVoicePlayerU3Ek__BackingField_5 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CVoicePlayerU3Ek__BackingField_5), (void*)L_0);
		return;
	}
}
// System.Void QFramework.AudioManager::OnSingletonInit()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_OnSingletonInit_m3D1C6E028F973E3F9E0B32824161D018CE54CE38 (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_U3COnSingletonInitU3Eb__8_0_m9192C129BBFAF7808E1443E232C0BB55C41ACF8E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_U3COnSingletonInitU3Eb__8_1_m363F918A1199AC07BB4E1053E6FEF1CE9BBA46B0_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_U3COnSingletonInitU3Eb__8_2_m4D6072A151B1CFAA3F52F76E5037F101A6BB399A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BindableProperty_1_Register_mA9B66CC57EE9D6586607598395E59C8DA968FE87_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SafeObjectPool_1_Init_mB381C8FD59EE0D59D0AB6A93C6CE4DBEA1C1CA43_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// SafeObjectPool<AudioPlayer>.Instance.Init(10, 1);
		SafeObjectPool_1_t3542EC4AA4871EFF26264DA85DCDB6C39D22A8AD* L_0;
		L_0 = SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421(SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421_RuntimeMethod_var);
		NullCheck(L_0);
		SafeObjectPool_1_Init_mB381C8FD59EE0D59D0AB6A93C6CE4DBEA1C1CA43(L_0, ((int32_t)10), 1, SafeObjectPool_1_Init_mB381C8FD59EE0D59D0AB6A93C6CE4DBEA1C1CA43_RuntimeMethod_var);
		// MusicPlayer = AudioPlayer.Allocate(AudioKit.Settings.MusicVolume);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_1;
		L_1 = AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20_inline(NULL);
		NullCheck(L_1);
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_2;
		L_2 = AudioKitSettings_get_MusicVolume_mC12F412255E5C5D80823C6B401EA3B76CEA9EC84_inline(L_1, NULL);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_3;
		L_3 = AudioPlayer_Allocate_m7E4B1CB936A2FB00F782DA45373DF120B5C35646(L_2, NULL);
		AudioManager_set_MusicPlayer_m19CF9C6DCBACE60B3B532458F590BE8B4A6B9ACF_inline(__this, L_3, NULL);
		// MusicPlayer.UsedCache = false;
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_4;
		L_4 = AudioManager_get_MusicPlayer_m8046BC707549A3B916F8C074D12CB7E6C79E27BA_inline(__this, NULL);
		NullCheck(L_4);
		AudioPlayer_set_UsedCache_m0D255C3A9BF12259B211DFCF4C155AE5EC2A2DEA_inline(L_4, (bool)0, NULL);
		// MusicPlayer.IsLoop = true;
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_5;
		L_5 = AudioManager_get_MusicPlayer_m8046BC707549A3B916F8C074D12CB7E6C79E27BA_inline(__this, NULL);
		NullCheck(L_5);
		AudioPlayer_set_IsLoop_mB1AC04B836A3D80C92450B55197BDDFF6463AC17_inline(L_5, (bool)1, NULL);
		// VoicePlayer = AudioPlayer.Allocate(AudioKit.Settings.VoiceVolume);
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_6;
		L_6 = AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20_inline(NULL);
		NullCheck(L_6);
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_7;
		L_7 = AudioKitSettings_get_VoiceVolume_m67A62365F356F9A35A31A385452EDBCBFB99F3C1_inline(L_6, NULL);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_8;
		L_8 = AudioPlayer_Allocate_m7E4B1CB936A2FB00F782DA45373DF120B5C35646(L_7, NULL);
		AudioManager_set_VoicePlayer_mE9427043D0CD26480779BA21020D4C22FD7E40BC_inline(__this, L_8, NULL);
		// VoicePlayer.UsedCache = false;
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_9;
		L_9 = AudioManager_get_VoicePlayer_m2FFBB17585A0BEEB38C67CF63A693A9F38C87D3D_inline(__this, NULL);
		NullCheck(L_9);
		AudioPlayer_set_UsedCache_m0D255C3A9BF12259B211DFCF4C155AE5EC2A2DEA_inline(L_9, (bool)0, NULL);
		// CheckAudioListener();
		AudioManager_CheckAudioListener_m886498C2B5C2966BB1776BA22ED94F0732BF7FA4(__this, NULL);
		// gameObject.transform.position = Vector3.zero;
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_10;
		L_10 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		NullCheck(L_10);
		Transform_tB27202C6F4E36D225EE28A13E4D662BF99785DB1* L_11;
		L_11 = GameObject_get_transform_m0BC10ADFA1632166AE5544BDF9038A2650C2AE56(L_10, NULL);
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_12;
		L_12 = Vector3_get_zero_m0C1249C3F25B1C70EAD3CC8B31259975A457AE39_inline(NULL);
		NullCheck(L_11);
		Transform_set_position_mA1A817124BB41B685043DED2A9BA48CDF37C4156(L_11, L_12, NULL);
		// AudioKit.Settings.IsMusicOn.Register(musicOn =>
		// {
		//     if (musicOn)
		//     {
		//         if (!string.IsNullOrEmpty(CurrentMusicName))
		//         {
		//             AudioKit.PlayMusic(CurrentMusicName);
		//         }
		//     }
		//     else
		//     {
		//         MusicPlayer.Stop();
		//     }
		// }).UnRegisterWhenGameObjectDestroyed(gameObject);
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_13;
		L_13 = AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20_inline(NULL);
		NullCheck(L_13);
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_14;
		L_14 = AudioKitSettings_get_IsMusicOn_mB0034F4E51AEAC7FF364B848A6A42CD30E1CBB89_inline(L_13, NULL);
		Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* L_15 = (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C*)il2cpp_codegen_object_new(Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C_il2cpp_TypeInfo_var);
		NullCheck(L_15);
		Action_1__ctor_mA8C3AC97D1F076EA5D1D0C10CEE6BD3E94711501(L_15, __this, (intptr_t)((void*)AudioManager_U3COnSingletonInitU3Eb__8_0_m9192C129BBFAF7808E1443E232C0BB55C41ACF8E_RuntimeMethod_var), NULL);
		NullCheck(L_14);
		RuntimeObject* L_16;
		L_16 = BindableProperty_1_Register_mA9B66CC57EE9D6586607598395E59C8DA968FE87(L_14, L_15, BindableProperty_1_Register_mA9B66CC57EE9D6586607598395E59C8DA968FE87_RuntimeMethod_var);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_17;
		L_17 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		RuntimeObject* L_18;
		L_18 = UnRegisterExtension_UnRegisterWhenGameObjectDestroyed_m8D6632ECF8FF05C690C60F2AC00C492A6B7BEBB7(L_16, L_17, NULL);
		// AudioKit.Settings.IsVoiceOn.Register(voiceOn =>
		// {
		//     if (voiceOn)
		//     {
		//         if (!string.IsNullOrEmpty(CurrentVoiceName))
		//         {
		//             AudioKit.PlayVoice(CurrentVoiceName);
		//         }
		//     }
		//     else
		//     {
		//         VoicePlayer.Stop();
		//     }
		// }).UnRegisterWhenGameObjectDestroyed(gameObject);
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_19;
		L_19 = AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20_inline(NULL);
		NullCheck(L_19);
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_20;
		L_20 = AudioKitSettings_get_IsVoiceOn_m40198BDFF0BAC22A00FF9A7D42A90D9AF6BDF937_inline(L_19, NULL);
		Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* L_21 = (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C*)il2cpp_codegen_object_new(Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C_il2cpp_TypeInfo_var);
		NullCheck(L_21);
		Action_1__ctor_mA8C3AC97D1F076EA5D1D0C10CEE6BD3E94711501(L_21, __this, (intptr_t)((void*)AudioManager_U3COnSingletonInitU3Eb__8_1_m363F918A1199AC07BB4E1053E6FEF1CE9BBA46B0_RuntimeMethod_var), NULL);
		NullCheck(L_20);
		RuntimeObject* L_22;
		L_22 = BindableProperty_1_Register_mA9B66CC57EE9D6586607598395E59C8DA968FE87(L_20, L_21, BindableProperty_1_Register_mA9B66CC57EE9D6586607598395E59C8DA968FE87_RuntimeMethod_var);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_23;
		L_23 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		RuntimeObject* L_24;
		L_24 = UnRegisterExtension_UnRegisterWhenGameObjectDestroyed_m8D6632ECF8FF05C690C60F2AC00C492A6B7BEBB7(L_22, L_23, NULL);
		// AudioKit.Settings.IsSoundOn.Register(soundOn =>
		// {
		//     if (soundOn)
		//     {
		//     }
		//     else
		//     {
		//         ForEachAllSound(player => player.Stop());
		//     }
		// }).UnRegisterWhenGameObjectDestroyed(gameObject);
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_25;
		L_25 = AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20_inline(NULL);
		NullCheck(L_25);
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_26;
		L_26 = AudioKitSettings_get_IsSoundOn_m16E5D82CD5CE9BDAA868820A75FA970239276A85_inline(L_25, NULL);
		Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C* L_27 = (Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C*)il2cpp_codegen_object_new(Action_1_t10DCB0C07D0D3C565CEACADC80D1152B35A45F6C_il2cpp_TypeInfo_var);
		NullCheck(L_27);
		Action_1__ctor_mA8C3AC97D1F076EA5D1D0C10CEE6BD3E94711501(L_27, __this, (intptr_t)((void*)AudioManager_U3COnSingletonInitU3Eb__8_2_m4D6072A151B1CFAA3F52F76E5037F101A6BB399A_RuntimeMethod_var), NULL);
		NullCheck(L_26);
		RuntimeObject* L_28;
		L_28 = BindableProperty_1_Register_mA9B66CC57EE9D6586607598395E59C8DA968FE87(L_26, L_27, BindableProperty_1_Register_mA9B66CC57EE9D6586607598395E59C8DA968FE87_RuntimeMethod_var);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_29;
		L_29 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		RuntimeObject* L_30;
		L_30 = UnRegisterExtension_UnRegisterWhenGameObjectDestroyed_m8D6632ECF8FF05C690C60F2AC00C492A6B7BEBB7(L_28, L_29, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioManager::ForEachAllSound(System.Action`1<QFramework.AudioPlayer>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_ForEachAllSound_m937C7CCCD694ECFFC9A32A0F90389A389B046EB2 (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* ___0_operation, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Enumerable_SelectMany_TisKeyValuePair_2_tA1ACD899BEC543AE9704AB31AC4C62160B215F43_TisAudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59_mAC2CE1EF65E76E2298ACF9EC5852CA617133628D_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerable_1_t84CEB7B86C0AC2FEB137388487931364B31CF457_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_1_t374204E15A93E57A3C60B540043B3B795485AC96_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec_U3CForEachAllSoundU3Eb__10_0_m7DFE197E012B617AADBCCC71F64625325C1A9248_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* V_1 = NULL;
	Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB* G_B2_0 = NULL;
	Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* G_B2_1 = NULL;
	Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB* G_B1_0 = NULL;
	Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* G_B1_1 = NULL;
	{
		// foreach (var audioPlayer in mSoundPlayerInPlaying.SelectMany(keyValuePair => keyValuePair.Value))
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* L_0 = ((AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_StaticFields*)il2cpp_codegen_static_fields_for(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var))->___mSoundPlayerInPlaying_6;
		il2cpp_codegen_runtime_class_init_inline(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var);
		Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB* L_1 = ((U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var))->___U3CU3E9__10_0_2;
		Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB* L_2 = L_1;
		G_B1_0 = L_2;
		G_B1_1 = L_0;
		if (L_2)
		{
			G_B2_0 = L_2;
			G_B2_1 = L_0;
			goto IL_0024;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var);
		U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86* L_3 = ((U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var))->___U3CU3E9_0;
		Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB* L_4 = (Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB*)il2cpp_codegen_object_new(Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		Func_2__ctor_mB667DF572ECCB07C1425B90525AFA36B56845C1C(L_4, L_3, (intptr_t)((void*)U3CU3Ec_U3CForEachAllSoundU3Eb__10_0_m7DFE197E012B617AADBCCC71F64625325C1A9248_RuntimeMethod_var), NULL);
		Func_2_t80C2C280589B34B77B403D18F5C2B7283D35F1DB* L_5 = L_4;
		((U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var))->___U3CU3E9__10_0_2 = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&((U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var))->___U3CU3E9__10_0_2), (void*)L_5);
		G_B2_0 = L_5;
		G_B2_1 = G_B1_1;
	}

IL_0024:
	{
		RuntimeObject* L_6;
		L_6 = Enumerable_SelectMany_TisKeyValuePair_2_tA1ACD899BEC543AE9704AB31AC4C62160B215F43_TisAudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59_mAC2CE1EF65E76E2298ACF9EC5852CA617133628D(G_B2_1, G_B2_0, Enumerable_SelectMany_TisKeyValuePair_2_tA1ACD899BEC543AE9704AB31AC4C62160B215F43_TisAudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59_mAC2CE1EF65E76E2298ACF9EC5852CA617133628D_RuntimeMethod_var);
		NullCheck(L_6);
		RuntimeObject* L_7;
		L_7 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* System.Collections.Generic.IEnumerator`1<T> System.Collections.Generic.IEnumerable`1<QFramework.AudioPlayer>::GetEnumerator() */, IEnumerable_1_t84CEB7B86C0AC2FEB137388487931364B31CF457_il2cpp_TypeInfo_var, L_6);
		V_0 = L_7;
	}
	{
		auto __finallyBlock = il2cpp::utils::Finally([&]
		{

FINALLY_0049:
			{// begin finally (depth: 1)
				{
					RuntimeObject* L_8 = V_0;
					if (!L_8)
					{
						goto IL_0052;
					}
				}
				{
					RuntimeObject* L_9 = V_0;
					NullCheck(L_9);
					InterfaceActionInvoker0::Invoke(0 /* System.Void System.IDisposable::Dispose() */, IDisposable_t030E0496B4E0E4E4F086825007979AF51F7248C5_il2cpp_TypeInfo_var, L_9);
				}

IL_0052:
				{
					return;
				}
			}// end finally (depth: 1)
		});
		try
		{// begin try (depth: 1)
			{
				goto IL_003f_1;
			}

IL_0031_1:
			{
				// foreach (var audioPlayer in mSoundPlayerInPlaying.SelectMany(keyValuePair => keyValuePair.Value))
				RuntimeObject* L_10 = V_0;
				NullCheck(L_10);
				AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_11;
				L_11 = InterfaceFuncInvoker0< AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* >::Invoke(0 /* T System.Collections.Generic.IEnumerator`1<QFramework.AudioPlayer>::get_Current() */, IEnumerator_1_t374204E15A93E57A3C60B540043B3B795485AC96_il2cpp_TypeInfo_var, L_10);
				V_1 = L_11;
				// operation(audioPlayer);
				Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_12 = ___0_operation;
				AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_13 = V_1;
				NullCheck(L_12);
				Action_1_Invoke_m0983EE053D0C9620014033A591393EE7C83C9A97_inline(L_12, L_13, NULL);
			}

IL_003f_1:
			{
				// foreach (var audioPlayer in mSoundPlayerInPlaying.SelectMany(keyValuePair => keyValuePair.Value))
				RuntimeObject* L_14 = V_0;
				NullCheck(L_14);
				bool L_15;
				L_15 = InterfaceFuncInvoker0< bool >::Invoke(0 /* System.Boolean System.Collections.IEnumerator::MoveNext() */, IEnumerator_t7B609C2FFA6EB5167D9C62A0C32A21DE2F666DAA_il2cpp_TypeInfo_var, L_14);
				if (L_15)
				{
					goto IL_0031_1;
				}
			}
			{
				goto IL_0053;
			}
		}// end try (depth: 1)
		catch(Il2CppExceptionWrapper& e)
		{
			__finallyBlock.StoreException(e.ex);
		}
	}

IL_0053:
	{
		// }
		return;
	}
}
// System.Void QFramework.AudioManager::AddSoundPlayer2Pool(QFramework.AudioPlayer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_AddSoundPlayer2Pool_m65FCF7B9091A32D9C58119B23787DDFEBB820B2E (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_audioPlayer, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_Add_mAC3ECB943BFF17C600E7EC191AB4A4E4C6E885F4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_ContainsKey_m6FE0713BCEFD16DC00E8636D5B8DD3BA313536E4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_get_Item_mF0187134582D2A345C1E9A834E336B5A5F96DEC9_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Add_m6320686C1369BBBA0D20DBC782155A5B2A00BAC5_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_mEB86DAF271C6934999B430DFFDA0C2B4BE67E398_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (mSoundPlayerInPlaying.ContainsKey(audioPlayer.GetName))
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* L_0 = ((AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_StaticFields*)il2cpp_codegen_static_fields_for(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var))->___mSoundPlayerInPlaying_6;
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_1 = ___0_audioPlayer;
		NullCheck(L_1);
		String_t* L_2;
		L_2 = AudioPlayer_get_GetName_mCD8D1164E27DDF8F85DD7DF840EF0E4C77340060_inline(L_1, NULL);
		NullCheck(L_0);
		bool L_3;
		L_3 = Dictionary_2_ContainsKey_m6FE0713BCEFD16DC00E8636D5B8DD3BA313536E4(L_0, L_2, Dictionary_2_ContainsKey_m6FE0713BCEFD16DC00E8636D5B8DD3BA313536E4_RuntimeMethod_var);
		if (!L_3)
		{
			goto IL_0029;
		}
	}
	{
		// mSoundPlayerInPlaying[audioPlayer.GetName].Add(audioPlayer);
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* L_4 = ((AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_StaticFields*)il2cpp_codegen_static_fields_for(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var))->___mSoundPlayerInPlaying_6;
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_5 = ___0_audioPlayer;
		NullCheck(L_5);
		String_t* L_6;
		L_6 = AudioPlayer_get_GetName_mCD8D1164E27DDF8F85DD7DF840EF0E4C77340060_inline(L_5, NULL);
		NullCheck(L_4);
		List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* L_7;
		L_7 = Dictionary_2_get_Item_mF0187134582D2A345C1E9A834E336B5A5F96DEC9(L_4, L_6, Dictionary_2_get_Item_mF0187134582D2A345C1E9A834E336B5A5F96DEC9_RuntimeMethod_var);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_8 = ___0_audioPlayer;
		NullCheck(L_7);
		List_1_Add_m6320686C1369BBBA0D20DBC782155A5B2A00BAC5_inline(L_7, L_8, List_1_Add_m6320686C1369BBBA0D20DBC782155A5B2A00BAC5_RuntimeMethod_var);
		return;
	}

IL_0029:
	{
		// mSoundPlayerInPlaying.Add(audioPlayer.GetName, new List<AudioPlayer> { audioPlayer });
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* L_9 = ((AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_StaticFields*)il2cpp_codegen_static_fields_for(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var))->___mSoundPlayerInPlaying_6;
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_10 = ___0_audioPlayer;
		NullCheck(L_10);
		String_t* L_11;
		L_11 = AudioPlayer_get_GetName_mCD8D1164E27DDF8F85DD7DF840EF0E4C77340060_inline(L_10, NULL);
		List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* L_12 = (List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D*)il2cpp_codegen_object_new(List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D_il2cpp_TypeInfo_var);
		NullCheck(L_12);
		List_1__ctor_mEB86DAF271C6934999B430DFFDA0C2B4BE67E398(L_12, List_1__ctor_mEB86DAF271C6934999B430DFFDA0C2B4BE67E398_RuntimeMethod_var);
		List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* L_13 = L_12;
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_14 = ___0_audioPlayer;
		NullCheck(L_13);
		List_1_Add_m6320686C1369BBBA0D20DBC782155A5B2A00BAC5_inline(L_13, L_14, List_1_Add_m6320686C1369BBBA0D20DBC782155A5B2A00BAC5_RuntimeMethod_var);
		NullCheck(L_9);
		Dictionary_2_Add_mAC3ECB943BFF17C600E7EC191AB4A4E4C6E885F4(L_9, L_11, L_13, Dictionary_2_Add_mAC3ECB943BFF17C600E7EC191AB4A4E4C6E885F4_RuntimeMethod_var);
		// }
		return;
	}
}
// System.Void QFramework.AudioManager::RemoveSoundPlayerFromPool(QFramework.AudioPlayer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_RemoveSoundPlayerFromPool_m144B7764E7D26C9EF81AE0F0C8E9BBAB74297E0E (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_audioPlayer, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_get_Item_mF0187134582D2A345C1E9A834E336B5A5F96DEC9_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Remove_m32474DB046D95F7ABA9D1985B34A186E4F41CDE0_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// mSoundPlayerInPlaying[audioPlayer.GetName].Remove(audioPlayer);
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* L_0 = ((AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_StaticFields*)il2cpp_codegen_static_fields_for(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var))->___mSoundPlayerInPlaying_6;
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_1 = ___0_audioPlayer;
		NullCheck(L_1);
		String_t* L_2;
		L_2 = AudioPlayer_get_GetName_mCD8D1164E27DDF8F85DD7DF840EF0E4C77340060_inline(L_1, NULL);
		NullCheck(L_0);
		List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* L_3;
		L_3 = Dictionary_2_get_Item_mF0187134582D2A345C1E9A834E336B5A5F96DEC9(L_0, L_2, Dictionary_2_get_Item_mF0187134582D2A345C1E9A834E336B5A5F96DEC9_RuntimeMethod_var);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_4 = ___0_audioPlayer;
		NullCheck(L_3);
		bool L_5;
		L_5 = List_1_Remove_m32474DB046D95F7ABA9D1985B34A186E4F41CDE0(L_3, L_4, List_1_Remove_m32474DB046D95F7ABA9D1985B34A186E4F41CDE0_RuntimeMethod_var);
		// }
		return;
	}
}
// System.Void QFramework.AudioManager::Init()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_Init_m4367BE4CC3F37C8D368D891EED28448E47F1C11B (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralE858D088F94FA9F7A1AC8D933236C82A8A396BEA);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Debug.Log("AudioManager.Init");
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_Log_m87A9A3C761FF5C43ED8A53B16190A53D08F818BB(_stringLiteralE858D088F94FA9F7A1AC8D933236C82A8A396BEA, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioManager::CheckAudioListener()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_CheckAudioListener_m886498C2B5C2966BB1776BA22ED94F0732BF7FA4 (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_AddComponent_TisAudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35_mA30AC51FE6287A9D0057077D99F694600A3D844E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_FindObjectOfType_TisAudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35_mFDA0D604F7239642B39B6010674A936ADD544912_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (!mAudioListener)
		AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35* L_0 = __this->___mAudioListener_7;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Implicit_m93896EF7D68FA113C42D3FE2BC6F661FC7EF514A(L_0, NULL);
		if (L_1)
		{
			goto IL_0018;
		}
	}
	{
		// mAudioListener = FindObjectOfType<AudioListener>();
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35* L_2;
		L_2 = Object_FindObjectOfType_TisAudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35_mFDA0D604F7239642B39B6010674A936ADD544912(Object_FindObjectOfType_TisAudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35_mFDA0D604F7239642B39B6010674A936ADD544912_RuntimeMethod_var);
		__this->___mAudioListener_7 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mAudioListener_7), (void*)L_2);
	}

IL_0018:
	{
		// if (!mAudioListener)
		AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35* L_3 = __this->___mAudioListener_7;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_4;
		L_4 = Object_op_Implicit_m93896EF7D68FA113C42D3FE2BC6F661FC7EF514A(L_3, NULL);
		if (L_4)
		{
			goto IL_0036;
		}
	}
	{
		// mAudioListener = gameObject.AddComponent<AudioListener>();
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_5;
		L_5 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(__this, NULL);
		NullCheck(L_5);
		AudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35* L_6;
		L_6 = GameObject_AddComponent_TisAudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35_mA30AC51FE6287A9D0057077D99F694600A3D844E(L_5, GameObject_AddComponent_TisAudioListener_t1D629CE9BC079C8ECDE8F822616E8A8E319EAE35_mA30AC51FE6287A9D0057077D99F694600A3D844E_RuntimeMethod_var);
		__this->___mAudioListener_7 = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mAudioListener_7), (void*)L_6);
	}

IL_0036:
	{
		// }
		return;
	}
}
// System.String QFramework.AudioManager::get_CurrentMusicName()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AudioManager_get_CurrentMusicName_m46E7270EA32A4B0CD9B4261AF7CB78A75C6444FB (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) 
{
	{
		// public string CurrentMusicName { get; set; }
		String_t* L_0 = __this->___U3CCurrentMusicNameU3Ek__BackingField_8;
		return L_0;
	}
}
// System.Void QFramework.AudioManager::set_CurrentMusicName(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_set_CurrentMusicName_mE05C1458C0BCB217A49C8ABD589B4D4A8E77087C (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		// public string CurrentMusicName { get; set; }
		String_t* L_0 = ___0_value;
		__this->___U3CCurrentMusicNameU3Ek__BackingField_8 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CCurrentMusicNameU3Ek__BackingField_8), (void*)L_0);
		return;
	}
}
// System.String QFramework.AudioManager::get_CurrentVoiceName()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AudioManager_get_CurrentVoiceName_m2A6026179BDD9F9E07A287B6935786E0CCF24FA4 (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) 
{
	{
		// public string CurrentVoiceName { get; set; }
		String_t* L_0 = __this->___U3CCurrentVoiceNameU3Ek__BackingField_9;
		return L_0;
	}
}
// System.Void QFramework.AudioManager::set_CurrentVoiceName(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_set_CurrentVoiceName_m8203991273B51FA9A0FD8E9361C2D512E73CB6E4 (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		// public string CurrentVoiceName { get; set; }
		String_t* L_0 = ___0_value;
		__this->___U3CCurrentVoiceNameU3Ek__BackingField_9 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CCurrentVoiceNameU3Ek__BackingField_9), (void*)L_0);
		return;
	}
}
// System.Void QFramework.AudioManager::PlayVoiceOnce(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_PlayVoiceOnce_mBB6A9BFA0660A352CAECCD77D95C59EC91B6D668 (String_t* ___0_voiceName, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (string.IsNullOrEmpty(voiceName))
		String_t* L_0 = ___0_voiceName;
		bool L_1;
		L_1 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_0, NULL);
		if (!L_1)
		{
			goto IL_0009;
		}
	}
	{
		// return;
		return;
	}

IL_0009:
	{
		// var unit = SafeObjectPool<AudioPlayer>.Instance.Allocate();
		SafeObjectPool_1_t3542EC4AA4871EFF26264DA85DCDB6C39D22A8AD* L_2;
		L_2 = SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421(SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421_RuntimeMethod_var);
		NullCheck(L_2);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_3;
		L_3 = VirtualFuncInvoker0< AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* >::Invoke(6 /* T QFramework.Pool`1<QFramework.AudioPlayer>::Allocate() */, L_2);
		// unit.SetAudio(Instance.gameObject, voiceName, false);
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_4;
		L_4 = AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA(NULL);
		NullCheck(L_4);
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_5;
		L_5 = Component_get_gameObject_m57AEFBB14DB39EC476F740BA000E170355DE691B(L_4, NULL);
		String_t* L_6 = ___0_voiceName;
		NullCheck(L_3);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_7;
		L_7 = AudioPlayer_SetAudio_mB62B93B946B1ED12976423AA09E6ED70827AB5ED(L_3, L_5, L_6, (bool)0, NULL);
		// }
		return;
	}
}
// QFramework.AudioManager QFramework.AudioManager::get_Instance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* AudioManager_get_Instance_m616A3E04E8F56A0C78643FBBBABC8F8BCBBFBCBA (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MonoSingletonProperty_1_get_Instance_m2BF4163558BC28C3B9119589FDD486ADC669CA7F_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static AudioManager Instance => MonoSingletonProperty<AudioManager>.Instance;
		AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* L_0;
		L_0 = MonoSingletonProperty_1_get_Instance_m2BF4163558BC28C3B9119589FDD486ADC669CA7F(MonoSingletonProperty_1_get_Instance_m2BF4163558BC28C3B9119589FDD486ADC669CA7F_RuntimeMethod_var);
		return L_0;
	}
}
// System.Void QFramework.AudioManager::ClearAllPlayingSound()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_ClearAllPlayingSound_mF5A45B5E7FC9FA2AAB1ECA1A5CEA41F812304278 (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_Clear_mAAB064453A17DFB0A15C203ECF907FFA7AF7D46E_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// mSoundPlayerInPlaying.Clear();
		il2cpp_codegen_runtime_class_init_inline(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* L_0 = ((AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_StaticFields*)il2cpp_codegen_static_fields_for(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var))->___mSoundPlayerInPlaying_6;
		NullCheck(L_0);
		Dictionary_2_Clear_mAAB064453A17DFB0A15C203ECF907FFA7AF7D46E(L_0, Dictionary_2_Clear_mAAB064453A17DFB0A15C203ECF907FFA7AF7D46E_RuntimeMethod_var);
		// }
		return;
	}
}
// System.Void QFramework.AudioManager::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager__ctor_m749EACDE5BCF17F36821D87F462B145D86B229BF (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
// System.Void QFramework.AudioManager::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager__cctor_m6D4EEB92360BF2636D960195B0AD86CCAC1C64E5 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2__ctor_mFE946D85F78DE391E71EA905A87F0BE5F34A041F_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// private static Dictionary<string, List<AudioPlayer>> mSoundPlayerInPlaying =
		//     new Dictionary<string, List<AudioPlayer>>(30);
		Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032* L_0 = (Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032*)il2cpp_codegen_object_new(Dictionary_2_tAE32178D8867DC2E490D088795BF6EF486E2A032_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		Dictionary_2__ctor_mFE946D85F78DE391E71EA905A87F0BE5F34A041F(L_0, ((int32_t)30), Dictionary_2__ctor_mFE946D85F78DE391E71EA905A87F0BE5F34A041F_RuntimeMethod_var);
		((AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_StaticFields*)il2cpp_codegen_static_fields_for(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var))->___mSoundPlayerInPlaying_6 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_StaticFields*)il2cpp_codegen_static_fields_for(AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274_il2cpp_TypeInfo_var))->___mSoundPlayerInPlaying_6), (void*)L_0);
		return;
	}
}
// System.Void QFramework.AudioManager::<OnSingletonInit>b__8_0(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_U3COnSingletonInitU3Eb__8_0_m9192C129BBFAF7808E1443E232C0BB55C41ACF8E (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, bool ___0_musicOn, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (musicOn)
		bool L_0 = ___0_musicOn;
		if (!L_0)
		{
			goto IL_0024;
		}
	}
	{
		// if (!string.IsNullOrEmpty(CurrentMusicName))
		String_t* L_1;
		L_1 = AudioManager_get_CurrentMusicName_m46E7270EA32A4B0CD9B4261AF7CB78A75C6444FB_inline(__this, NULL);
		bool L_2;
		L_2 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_1, NULL);
		if (L_2)
		{
			goto IL_002f;
		}
	}
	{
		// AudioKit.PlayMusic(CurrentMusicName);
		String_t* L_3;
		L_3 = AudioManager_get_CurrentMusicName_m46E7270EA32A4B0CD9B4261AF7CB78A75C6444FB_inline(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKit_PlayMusic_m024F70A37392A7DB77FD290AAA197DCF0FA7B8F6(L_3, (bool)1, (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL, (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL, (1.0f), NULL);
		return;
	}

IL_0024:
	{
		// MusicPlayer.Stop();
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_4;
		L_4 = AudioManager_get_MusicPlayer_m8046BC707549A3B916F8C074D12CB7E6C79E27BA_inline(__this, NULL);
		NullCheck(L_4);
		AudioPlayer_Stop_m76865190573766D55E4533B8AEA5AE7F8834E96B(L_4, NULL);
	}

IL_002f:
	{
		// }).UnRegisterWhenGameObjectDestroyed(gameObject);
		return;
	}
}
// System.Void QFramework.AudioManager::<OnSingletonInit>b__8_1(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_U3COnSingletonInitU3Eb__8_1_m363F918A1199AC07BB4E1053E6FEF1CE9BBA46B0 (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, bool ___0_voiceOn, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (voiceOn)
		bool L_0 = ___0_voiceOn;
		if (!L_0)
		{
			goto IL_001f;
		}
	}
	{
		// if (!string.IsNullOrEmpty(CurrentVoiceName))
		String_t* L_1;
		L_1 = AudioManager_get_CurrentVoiceName_m2A6026179BDD9F9E07A287B6935786E0CCF24FA4_inline(__this, NULL);
		bool L_2;
		L_2 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_1, NULL);
		if (L_2)
		{
			goto IL_002a;
		}
	}
	{
		// AudioKit.PlayVoice(CurrentVoiceName);
		String_t* L_3;
		L_3 = AudioManager_get_CurrentVoiceName_m2A6026179BDD9F9E07A287B6935786E0CCF24FA4_inline(__this, NULL);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKit_PlayVoice_m0DB27FC1DA555F0F196CD00346055B002B7DAC92(L_3, (bool)0, (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL, (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL, NULL);
		return;
	}

IL_001f:
	{
		// VoicePlayer.Stop();
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_4;
		L_4 = AudioManager_get_VoicePlayer_m2FFBB17585A0BEEB38C67CF63A693A9F38C87D3D_inline(__this, NULL);
		NullCheck(L_4);
		AudioPlayer_Stop_m76865190573766D55E4533B8AEA5AE7F8834E96B(L_4, NULL);
	}

IL_002a:
	{
		// }).UnRegisterWhenGameObjectDestroyed(gameObject);
		return;
	}
}
// System.Void QFramework.AudioManager::<OnSingletonInit>b__8_2(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioManager_U3COnSingletonInitU3Eb__8_2_m4D6072A151B1CFAA3F52F76E5037F101A6BB399A (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, bool ___0_soundOn, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec_U3COnSingletonInitU3Eb__8_3_mABE712C9E196449F677BA9735EBDE446C92C1360_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* G_B3_0 = NULL;
	AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* G_B3_1 = NULL;
	Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* G_B2_0 = NULL;
	AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* G_B2_1 = NULL;
	{
		// if (soundOn)
		bool L_0 = ___0_soundOn;
		if (L_0)
		{
			goto IL_0028;
		}
	}
	{
		// ForEachAllSound(player => player.Stop());
		il2cpp_codegen_runtime_class_init_inline(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var);
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_1 = ((U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var))->___U3CU3E9__8_3_1;
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_2 = L_1;
		G_B2_0 = L_2;
		G_B2_1 = __this;
		if (L_2)
		{
			G_B3_0 = L_2;
			G_B3_1 = __this;
			goto IL_0023;
		}
	}
	{
		il2cpp_codegen_runtime_class_init_inline(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var);
		U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86* L_3 = ((U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var))->___U3CU3E9_0;
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_4 = (Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219*)il2cpp_codegen_object_new(Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		Action_1__ctor_m3C560AF46A6955D28CF1A054DD88FD769247FDF0(L_4, L_3, (intptr_t)((void*)U3CU3Ec_U3COnSingletonInitU3Eb__8_3_mABE712C9E196449F677BA9735EBDE446C92C1360_RuntimeMethod_var), NULL);
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_5 = L_4;
		((U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var))->___U3CU3E9__8_3_1 = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&((U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var))->___U3CU3E9__8_3_1), (void*)L_5);
		G_B3_0 = L_5;
		G_B3_1 = G_B2_1;
	}

IL_0023:
	{
		NullCheck(G_B3_1);
		AudioManager_ForEachAllSound_m937C7CCCD694ECFFC9A32A0F90389A389B046EB2(G_B3_1, G_B3_0, NULL);
	}

IL_0028:
	{
		// }).UnRegisterWhenGameObjectDestroyed(gameObject);
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
// System.Void QFramework.AudioManager/<>c::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__cctor_mB2E0542AB87AB71CB9ABCC0CF06609A6A56F929C (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86* L_0 = (U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86*)il2cpp_codegen_object_new(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__ctor_m0823286159044D6323AB819D03043C253523655C(L_0, NULL);
		((U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var))->___U3CU3E9_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_StaticFields*)il2cpp_codegen_static_fields_for(U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86_il2cpp_TypeInfo_var))->___U3CU3E9_0), (void*)L_0);
		return;
	}
}
// System.Void QFramework.AudioManager/<>c::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__ctor_m0823286159044D6323AB819D03043C253523655C (U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void QFramework.AudioManager/<>c::<OnSingletonInit>b__8_3(QFramework.AudioPlayer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec_U3COnSingletonInitU3Eb__8_3_mABE712C9E196449F677BA9735EBDE446C92C1360 (U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_player, const RuntimeMethod* method) 
{
	{
		// ForEachAllSound(player => player.Stop());
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_0 = ___0_player;
		NullCheck(L_0);
		AudioPlayer_Stop_m76865190573766D55E4533B8AEA5AE7F8834E96B(L_0, NULL);
		return;
	}
}
// System.Collections.Generic.IEnumerable`1<QFramework.AudioPlayer> QFramework.AudioManager/<>c::<ForEachAllSound>b__10_0(System.Collections.Generic.KeyValuePair`2<System.String,System.Collections.Generic.List`1<QFramework.AudioPlayer>>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR RuntimeObject* U3CU3Ec_U3CForEachAllSoundU3Eb__10_0_m7DFE197E012B617AADBCCC71F64625325C1A9248 (U3CU3Ec_t396C477B755A7E0B1EAE5284F6E7E8B1EC64BD86* __this, KeyValuePair_2_tA1ACD899BEC543AE9704AB31AC4C62160B215F43 ___0_keyValuePair, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&KeyValuePair_2_get_Value_m267D88C38008E34C628B6E9037CB9FB98C40C334_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// foreach (var audioPlayer in mSoundPlayerInPlaying.SelectMany(keyValuePair => keyValuePair.Value))
		List_1_t997CC9CFD19A71338522B20AA0DE61BF8BE2486D* L_0;
		L_0 = KeyValuePair_2_get_Value_m267D88C38008E34C628B6E9037CB9FB98C40C334_inline((&___0_keyValuePair), KeyValuePair_2_get_Value_m267D88C38008E34C628B6E9037CB9FB98C40C334_RuntimeMethod_var);
		return L_0;
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
// QFramework.BindableProperty`1<System.Single> QFramework.AudioPlayer::get_Volume()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* AudioPlayer_get_Volume_m2095A2461F76393AA590905BEB51E7E90A174126 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// public BindableProperty<float> Volume { get; set; }
		BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* L_0 = __this->___U3CVolumeU3Ek__BackingField_3;
		return L_0;
	}
}
// System.Void QFramework.AudioPlayer::set_Volume(QFramework.BindableProperty`1<System.Single>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_set_Volume_m8D35738EFB189D11C6FBFA3A1C116B3C4D007F5E (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* ___0_value, const RuntimeMethod* method) 
{
	{
		// public BindableProperty<float> Volume { get; set; }
		BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* L_0 = ___0_value;
		__this->___U3CVolumeU3Ek__BackingField_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CVolumeU3Ek__BackingField_3), (void*)L_0);
		return;
	}
}
// System.String QFramework.AudioPlayer::get_GetName()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR String_t* AudioPlayer_get_GetName_mCD8D1164E27DDF8F85DD7DF840EF0E4C77340060 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// public string GetName => mName;
		String_t* L_0 = __this->___mName_2;
		return L_0;
	}
}
// UnityEngine.AudioSource QFramework.AudioPlayer::get_AudioSource()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* AudioPlayer_get_AudioSource_mA8FC1A3A14798CA0FBD13F8E24A3F48C8631E132 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// public AudioSource AudioSource => mAudioSource;
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_0 = __this->___mAudioSource_1;
		return L_0;
	}
}
// QFramework.AudioPlayer QFramework.AudioPlayer::Allocate(QFramework.BindableProperty`1<System.Single>)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioPlayer_Allocate_m7E4B1CB936A2FB00F782DA45373DF120B5C35646 (BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* ___0_volume, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// var player =  SafeObjectPool<AudioPlayer>.Instance.Allocate();
		SafeObjectPool_1_t3542EC4AA4871EFF26264DA85DCDB6C39D22A8AD* L_0;
		L_0 = SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421(SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421_RuntimeMethod_var);
		NullCheck(L_0);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_1;
		L_1 = VirtualFuncInvoker0< AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* >::Invoke(6 /* T QFramework.Pool`1<QFramework.AudioPlayer>::Allocate() */, L_0);
		// player.Volume = volume;
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_2 = L_1;
		BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* L_3 = ___0_volume;
		NullCheck(L_2);
		AudioPlayer_set_Volume_m8D35738EFB189D11C6FBFA3A1C116B3C4D007F5E_inline(L_2, L_3, NULL);
		// player.OnAllocate();
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_4 = L_2;
		NullCheck(L_4);
		AudioPlayer_OnAllocate_m9B7CA46E03DC8460D4BC05D1162E5EC0D4989C33(L_4, NULL);
		// return player;
		return L_4;
	}
}
// System.Void QFramework.AudioPlayer::OnAllocate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_OnAllocate_m9B7CA46E03DC8460D4BC05D1162E5EC0D4989C33 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioPlayer_SetVolume_m0306F9423FAFF5F273E941A297F11EC77D516260_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BindableProperty_1_RegisterWithInitValue_m50618DDD0FF1AADCBFF8D781BC9D6DE5AEA35824_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// mOnStart = null;
		__this->___mOnStart_6 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mOnStart_6), (void*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL);
		// mOnFinish = null;
		__this->___mOnFinish_7 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mOnFinish_7), (void*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL);
		// mVolume = 1.0f;
		__this->___mVolume_11 = (1.0f);
		// mVolumeScale = 1.0f;
		__this->___mVolumeScale_10 = (1.0f);
		// Volume.RegisterWithInitValue(SetVolume);
		BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* L_0;
		L_0 = AudioPlayer_get_Volume_m2095A2461F76393AA590905BEB51E7E90A174126_inline(__this, NULL);
		Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* L_1 = (Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A*)il2cpp_codegen_object_new(Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		Action_1__ctor_m770CD2F8BB65F2EDA5128CA2F96D71C35B23E859(L_1, __this, (intptr_t)((void*)AudioPlayer_SetVolume_m0306F9423FAFF5F273E941A297F11EC77D516260_RuntimeMethod_var), NULL);
		NullCheck(L_0);
		RuntimeObject* L_2;
		L_2 = BindableProperty_1_RegisterWithInitValue_m50618DDD0FF1AADCBFF8D781BC9D6DE5AEA35824(L_0, L_1, BindableProperty_1_RegisterWithInitValue_m50618DDD0FF1AADCBFF8D781BC9D6DE5AEA35824_RuntimeMethod_var);
		// }
		return;
	}
}
// QFramework.AudioPlayer QFramework.AudioPlayer::OnStart(System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioPlayer_OnStart_m6B06144CA7EE9C7117AE884B1EBBAEA727F6079D (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___0_onStart, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (onStart == null) return this;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ___0_onStart;
		if (L_0)
		{
			goto IL_0005;
		}
	}
	{
		// if (onStart == null) return this;
		return __this;
	}

IL_0005:
	{
		// if (mOnStart == null)
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = __this->___mOnStart_6;
		if (L_1)
		{
			goto IL_0016;
		}
	}
	{
		// mOnStart = onStart;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_2 = ___0_onStart;
		__this->___mOnStart_6 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mOnStart_6), (void*)L_2);
		goto IL_002d;
	}

IL_0016:
	{
		// mOnStart += onStart;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_3 = __this->___mOnStart_6;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_4 = ___0_onStart;
		Delegate_t* L_5;
		L_5 = Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00(L_3, L_4, NULL);
		__this->___mOnStart_6 = ((Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)CastclassSealed((RuntimeObject*)L_5, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var));
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mOnStart_6), (void*)((Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)CastclassSealed((RuntimeObject*)L_5, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var)));
	}

IL_002d:
	{
		// return this;
		return __this;
	}
}
// System.Void QFramework.AudioPlayer::QFramework.IAudioKitOnFinish.OnFinish(System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_QFramework_IAudioKitOnFinish_OnFinish_m88B5F240F97426B432F5DA26294577B1968C40D9 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___0_onFinish, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (onFinish == null) return;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = ___0_onFinish;
		if (L_0)
		{
			goto IL_0004;
		}
	}
	{
		// if (onFinish == null) return;
		return;
	}

IL_0004:
	{
		// if (mOnFinish == null)
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = __this->___mOnFinish_7;
		if (L_1)
		{
			goto IL_0014;
		}
	}
	{
		// mOnFinish = onFinish;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_2 = ___0_onFinish;
		__this->___mOnFinish_7 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mOnFinish_7), (void*)L_2);
		return;
	}

IL_0014:
	{
		// mOnFinish += onFinish;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_3 = __this->___mOnFinish_7;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_4 = ___0_onFinish;
		Delegate_t* L_5;
		L_5 = Delegate_Combine_m1F725AEF318BE6F0426863490691A6F4606E7D00(L_3, L_4, NULL);
		__this->___mOnFinish_7 = ((Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)CastclassSealed((RuntimeObject*)L_5, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var));
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mOnFinish_7), (void*)((Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)CastclassSealed((RuntimeObject*)L_5, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var)));
		// }
		return;
	}
}
// System.Boolean QFramework.AudioPlayer::get_UsedCache()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioPlayer_get_UsedCache_m90781A61EB4E15DE4B973135C4D973D661351885 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// public bool UsedCache { get; set; } = true;
		bool L_0 = __this->___U3CUsedCacheU3Ek__BackingField_12;
		return L_0;
	}
}
// System.Void QFramework.AudioPlayer::set_UsedCache(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_set_UsedCache_m0D255C3A9BF12259B211DFCF4C155AE5EC2A2DEA (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		// public bool UsedCache { get; set; } = true;
		bool L_0 = ___0_value;
		__this->___U3CUsedCacheU3Ek__BackingField_12 = L_0;
		return;
	}
}
// System.Boolean QFramework.AudioPlayer::get_IsRecycled()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioPlayer_get_IsRecycled_mEF70EC8CD834AA6A27F1C32629E9BF64F2FA5C53 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsRecycled { get; set; } = false;
		bool L_0 = __this->___U3CIsRecycledU3Ek__BackingField_13;
		return L_0;
	}
}
// System.Void QFramework.AudioPlayer::set_IsRecycled(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_set_IsRecycled_m960B558D3CEE82CDAA3AD0B0169DEFFA4D662E9B (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		// public bool IsRecycled { get; set; } = false;
		bool L_0 = ___0_value;
		__this->___U3CIsRecycledU3Ek__BackingField_13 = L_0;
		return;
	}
}
// System.Boolean QFramework.AudioPlayer::get_IsLoop()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool AudioPlayer_get_IsLoop_mC43549D534C455179A16FF1C2EDA241B117717E7 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsLoop { get; set; }
		bool L_0 = __this->___U3CIsLoopU3Ek__BackingField_14;
		return L_0;
	}
}
// System.Void QFramework.AudioPlayer::set_IsLoop(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_set_IsLoop_mB1AC04B836A3D80C92450B55197BDDFF6463AC17 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		// public bool IsLoop { get; set; }
		bool L_0 = ___0_value;
		__this->___U3CIsLoopU3Ek__BackingField_14 = L_0;
		return;
	}
}
// System.Void QFramework.AudioPlayer::SetAudioExt(UnityEngine.GameObject,UnityEngine.AudioClip,System.String,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_SetAudioExt_m159E924BA175BD34E5A784D5B274816B100994C7 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___0_root, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, String_t* ___2_name, bool ___3_loop, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_AddComponent_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_m0E8EFDB9B3D8DF1ADE10C56D3168A9C1BA19BF14_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (clip == null || mName == name)
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_0 = ___1_clip;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (L_1)
		{
			goto IL_0017;
		}
	}
	{
		String_t* L_2 = __this->___mName_2;
		String_t* L_3 = ___2_name;
		bool L_4;
		L_4 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_2, L_3, NULL);
		if (!L_4)
		{
			goto IL_0018;
		}
	}

IL_0017:
	{
		// return;
		return;
	}

IL_0018:
	{
		// if (mAudioSource == null)
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_5 = __this->___mAudioSource_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_6;
		L_6 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_5, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_6)
		{
			goto IL_0032;
		}
	}
	{
		// mAudioSource = root.AddComponent<AudioSource>();
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_7 = ___0_root;
		NullCheck(L_7);
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_8;
		L_8 = GameObject_AddComponent_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_m0E8EFDB9B3D8DF1ADE10C56D3168A9C1BA19BF14(L_7, GameObject_AddComponent_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_m0E8EFDB9B3D8DF1ADE10C56D3168A9C1BA19BF14_RuntimeMethod_var);
		__this->___mAudioSource_1 = L_8;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mAudioSource_1), (void*)L_8);
	}

IL_0032:
	{
		// CleanResources();
		AudioPlayer_CleanResources_mC72F4BF993C81EADE75AA24B40E3D39A3ACB4E63(__this, NULL);
		// IsLoop = loop;
		bool L_9 = ___3_loop;
		AudioPlayer_set_IsLoop_mB1AC04B836A3D80C92450B55197BDDFF6463AC17_inline(__this, L_9, NULL);
		// mName = name;
		String_t* L_10 = ___2_name;
		__this->___mName_2 = L_10;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mName_2), (void*)L_10);
		// mAudioClip = clip;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_11 = ___1_clip;
		__this->___mAudioClip_4 = L_11;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mAudioClip_4), (void*)L_11);
		// PlayAudioClip();
		AudioPlayer_PlayAudioClip_mE75B2E82ED39EC2C929277F8342A2749FFDE80F6(__this, NULL);
		// }
		return;
	}
}
// QFramework.AudioPlayer QFramework.AudioPlayer::SetAudio(UnityEngine.GameObject,System.String,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioPlayer_SetAudio_mB62B93B946B1ED12976423AA09E6ED70827AB5ED (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* ___0_root, String_t* ___1_name, bool ___2_loop, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioPlayer_OnResLoadFinish_m62EB8A101B47B4E722D75F817C37ED012B1A33B4_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_AddComponent_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_m0E8EFDB9B3D8DF1ADE10C56D3168A9C1BA19BF14_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IAudioLoaderPool_t25A31F4FC2301F1B46FC33A0BF0C2E1F65589A22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IAudioLoader_t02005AF5330B4CD269CC97AB97F7A9510BE4D79A_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	RuntimeObject* V_0 = NULL;
	AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* V_1 = NULL;
	{
		// if (string.IsNullOrEmpty(name))
		String_t* L_0 = ___1_name;
		bool L_1;
		L_1 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_0, NULL);
		if (!L_1)
		{
			goto IL_000a;
		}
	}
	{
		// return this;
		return __this;
	}

IL_000a:
	{
		// if (mName == name)
		String_t* L_2 = __this->___mName_2;
		String_t* L_3 = ___1_name;
		bool L_4;
		L_4 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_2, L_3, NULL);
		if (!L_4)
		{
			goto IL_001a;
		}
	}
	{
		// return this;
		return __this;
	}

IL_001a:
	{
		// if (mAudioSource == null)
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_5 = __this->___mAudioSource_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_6;
		L_6 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_5, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_6)
		{
			goto IL_0034;
		}
	}
	{
		// mAudioSource = root.AddComponent<AudioSource>();
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_7 = ___0_root;
		NullCheck(L_7);
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_8;
		L_8 = GameObject_AddComponent_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_m0E8EFDB9B3D8DF1ADE10C56D3168A9C1BA19BF14(L_7, GameObject_AddComponent_TisAudioSource_t871AC2272F896738252F04EE949AEF5B241D3299_m0E8EFDB9B3D8DF1ADE10C56D3168A9C1BA19BF14_RuntimeMethod_var);
		__this->___mAudioSource_1 = L_8;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mAudioSource_1), (void*)L_8);
	}

IL_0034:
	{
		// var preLoader = mLoader;
		RuntimeObject* L_9 = __this->___mLoader_0;
		V_0 = L_9;
		// mLoader = null;
		__this->___mLoader_0 = (RuntimeObject*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mLoader_0), (void*)(RuntimeObject*)NULL);
		// CleanResources();
		AudioPlayer_CleanResources_mC72F4BF993C81EADE75AA24B40E3D39A3ACB4E63(__this, NULL);
		// mLoader = AudioKit.Config.AudioLoaderPool.AllocateLoader();
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKitConfig_t0518724CE4F14A672627665BE74D533751686A54* L_10 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___Config_0;
		NullCheck(L_10);
		RuntimeObject* L_11 = L_10->___AudioLoaderPool_0;
		NullCheck(L_11);
		RuntimeObject* L_12;
		L_12 = InterfaceFuncInvoker0< RuntimeObject* >::Invoke(0 /* QFramework.IAudioLoader QFramework.IAudioLoaderPool::AllocateLoader() */, IAudioLoaderPool_t25A31F4FC2301F1B46FC33A0BF0C2E1F65589A22_il2cpp_TypeInfo_var, L_11);
		__this->___mLoader_0 = L_12;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mLoader_0), (void*)L_12);
		// IsLoop = loop;
		bool L_13 = ___2_loop;
		AudioPlayer_set_IsLoop_mB1AC04B836A3D80C92450B55197BDDFF6463AC17_inline(__this, L_13, NULL);
		// mName = name;
		String_t* L_14 = ___1_name;
		__this->___mName_2 = L_14;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mName_2), (void*)L_14);
		// var keys = AudioSearchKeys.Allocate();
		AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* L_15;
		L_15 = AudioSearchKeys_Allocate_mCE7D3F47B99F8F3EDF78A16B3031F9D93EC906D1(NULL);
		V_1 = L_15;
		// keys.AssetName = name;
		AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* L_16 = V_1;
		String_t* L_17 = ___1_name;
		NullCheck(L_16);
		L_16->___AssetName_1 = L_17;
		Il2CppCodeGenWriteBarrier((void**)(&L_16->___AssetName_1), (void*)L_17);
		// mLoader.LoadClipAsync(keys, OnResLoadFinish);
		RuntimeObject* L_18 = __this->___mLoader_0;
		AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* L_19 = V_1;
		Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD* L_20 = (Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD*)il2cpp_codegen_object_new(Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD_il2cpp_TypeInfo_var);
		NullCheck(L_20);
		Action_2__ctor_mF42C97535261A64EA55EAADD09299B03C2D87D61(L_20, __this, (intptr_t)((void*)AudioPlayer_OnResLoadFinish_m62EB8A101B47B4E722D75F817C37ED012B1A33B4_RuntimeMethod_var), NULL);
		NullCheck(L_18);
		InterfaceActionInvoker2< AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3*, Action_2_t6A4E3643FE564F48CFB0053871C995C1275FBEFD* >::Invoke(2 /* System.Void QFramework.IAudioLoader::LoadClipAsync(QFramework.AudioSearchKeys,System.Action`2<System.Boolean,UnityEngine.AudioClip>) */, IAudioLoader_t02005AF5330B4CD269CC97AB97F7A9510BE4D79A_il2cpp_TypeInfo_var, L_18, L_19, L_20);
		// keys.Recycle2Cache();
		AudioSearchKeys_tC872BFEEC677163B1A7776A3A563FEABE4B035B3* L_21 = V_1;
		NullCheck(L_21);
		AudioSearchKeys_Recycle2Cache_mD5F065BE8C2A1E01CFF642CF29FE8F969F0835E9(L_21, NULL);
		// if (preLoader != null)
		RuntimeObject* L_22 = V_0;
		if (!L_22)
		{
			goto IL_00b1;
		}
	}
	{
		// preLoader.Unload();
		RuntimeObject* L_23 = V_0;
		NullCheck(L_23);
		InterfaceActionInvoker0::Invoke(3 /* System.Void QFramework.IAudioLoader::Unload() */, IAudioLoader_t02005AF5330B4CD269CC97AB97F7A9510BE4D79A_il2cpp_TypeInfo_var, L_23);
		// AudioKit.Config.AudioLoaderPool.RecycleLoader(preLoader);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKitConfig_t0518724CE4F14A672627665BE74D533751686A54* L_24 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___Config_0;
		NullCheck(L_24);
		RuntimeObject* L_25 = L_24->___AudioLoaderPool_0;
		RuntimeObject* L_26 = V_0;
		NullCheck(L_25);
		InterfaceActionInvoker1< RuntimeObject* >::Invoke(1 /* System.Void QFramework.IAudioLoaderPool::RecycleLoader(QFramework.IAudioLoader) */, IAudioLoaderPool_t25A31F4FC2301F1B46FC33A0BF0C2E1F65589A22_il2cpp_TypeInfo_var, L_25, L_26);
		// preLoader = null;
		V_0 = (RuntimeObject*)NULL;
	}

IL_00b1:
	{
		// return this;
		return __this;
	}
}
// System.Void QFramework.AudioPlayer::Stop()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_Stop_m76865190573766D55E4533B8AEA5AE7F8834E96B (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// Release();
		AudioPlayer_Release_mD740E38F2E434B0B6F98C8F93F19F498D6318595(__this, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioPlayer::Pause()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_Pause_mBFD7FA6BAA2AC4F968192F4F8A7FA8188EF865BC (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// if (mIsPause)
		bool L_0 = __this->___mIsPause_8;
		if (!L_0)
		{
			goto IL_0009;
		}
	}
	{
		// return;
		return;
	}

IL_0009:
	{
		// mLeftDelayTime = -1;
		__this->___mLeftDelayTime_9 = (-1.0f);
		// if (mTimeItem != null)
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_1 = __this->___mTimeItem_5;
		if (!L_1)
		{
			goto IL_004a;
		}
	}
	{
		// mLeftDelayTime = mTimeItem.SortScore - Timer.Instance.CurrentScaleTime;
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_2 = __this->___mTimeItem_5;
		NullCheck(L_2);
		float L_3;
		L_3 = TimeItem_get_SortScore_m77DBD8ADA7598C757D444823787D70A7FB5F18CF_inline(L_2, NULL);
		Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* L_4;
		L_4 = Timer_get_Instance_m20E57C957705CF1168B025EEAB98FAF69631694D(NULL);
		NullCheck(L_4);
		float L_5;
		L_5 = Timer_get_CurrentScaleTime_m7BA42A1AC216700628563A32EAF29F8571ADF3AA_inline(L_4, NULL);
		__this->___mLeftDelayTime_9 = ((float)il2cpp_codegen_subtract(L_3, L_5));
		// mTimeItem.Cancel();
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_6 = __this->___mTimeItem_5;
		NullCheck(L_6);
		TimeItem_Cancel_m37972167491A71932F518C5FECA2F73EE17CB62B(L_6, NULL);
		// mTimeItem = null;
		__this->___mTimeItem_5 = (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mTimeItem_5), (void*)(TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8*)NULL);
	}

IL_004a:
	{
		// mIsPause = true;
		__this->___mIsPause_8 = (bool)1;
		// mAudioSource.Pause();
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_7 = __this->___mAudioSource_1;
		NullCheck(L_7);
		AudioSource_Pause_m2C2A09359E8AA924FEADECC1AFEA519B3C915B26(L_7, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioPlayer::Resume()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_Resume_mC8F085A40D083FA2C0C2BE6D02492C58118F0522 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioPlayer_OnResumeTimeTick_m82DE3AC83904EE1C80B30D28C5B00BC6CBEA04B2_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (!mIsPause)
		bool L_0 = __this->___mIsPause_8;
		if (L_0)
		{
			goto IL_0009;
		}
	}
	{
		// return;
		return;
	}

IL_0009:
	{
		// if (mLeftDelayTime >= 0)
		float L_1 = __this->___mLeftDelayTime_9;
		if ((!(((float)L_1) >= ((float)(0.0f)))))
		{
			goto IL_0038;
		}
	}
	{
		// mTimeItem = Timer.Instance.Post2Scale(OnResumeTimeTick, mLeftDelayTime);
		Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* L_2;
		L_2 = Timer_get_Instance_m20E57C957705CF1168B025EEAB98FAF69631694D(NULL);
		Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* L_3 = (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404*)il2cpp_codegen_object_new(Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404_il2cpp_TypeInfo_var);
		NullCheck(L_3);
		Action_1__ctor_m1BA854F3F4319EA4A4294DDFDA21C395B8D0FF87(L_3, __this, (intptr_t)((void*)AudioPlayer_OnResumeTimeTick_m82DE3AC83904EE1C80B30D28C5B00BC6CBEA04B2_RuntimeMethod_var), NULL);
		float L_4 = __this->___mLeftDelayTime_9;
		NullCheck(L_2);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_5;
		L_5 = Timer_Post2Scale_m61CE9C095888EC19B9CB9B07CF2B81B446960B32(L_2, L_3, L_4, NULL);
		__this->___mTimeItem_5 = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mTimeItem_5), (void*)L_5);
	}

IL_0038:
	{
		// mIsPause = false;
		__this->___mIsPause_8 = (bool)0;
		// mAudioSource.Play();
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_6 = __this->___mAudioSource_1;
		NullCheck(L_6);
		AudioSource_Play_m95DF07111C61D0E0F00257A00384D31531D590C3(L_6, NULL);
		// }
		return;
	}
}
// QFramework.AudioPlayer QFramework.AudioPlayer::VolumeScale(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioPlayer_VolumeScale_mDD9640F3723126252B95FC8D6C3825E4E77FFC38 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, float ___0_volumeScale, const RuntimeMethod* method) 
{
	{
		// mVolumeScale = volumeScale;
		float L_0 = ___0_volumeScale;
		__this->___mVolumeScale_10 = L_0;
		// UpdateVolume();
		AudioPlayer_UpdateVolume_m2D4666F4768B21FEF1D9B2AE1E34063F343552A9(__this, NULL);
		// return this;
		return __this;
	}
}
// System.Void QFramework.AudioPlayer::SetVolume(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_SetVolume_m0306F9423FAFF5F273E941A297F11EC77D516260 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, float ___0_volume, const RuntimeMethod* method) 
{
	{
		// mVolume = volume;
		float L_0 = ___0_volume;
		__this->___mVolume_11 = L_0;
		// UpdateVolume();
		AudioPlayer_UpdateVolume_m2D4666F4768B21FEF1D9B2AE1E34063F343552A9(__this, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioPlayer::UpdateVolume()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_UpdateVolume_m2D4666F4768B21FEF1D9B2AE1E34063F343552A9 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (mAudioSource)
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_0 = __this->___mAudioSource_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Implicit_m93896EF7D68FA113C42D3FE2BC6F661FC7EF514A(L_0, NULL);
		if (!L_1)
		{
			goto IL_0025;
		}
	}
	{
		// mAudioSource.volume = mVolume * mVolumeScale;
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_2 = __this->___mAudioSource_1;
		float L_3 = __this->___mVolume_11;
		float L_4 = __this->___mVolumeScale_10;
		NullCheck(L_2);
		AudioSource_set_volume_mD902BBDBBDE0E3C148609BF3C05096148E90F2C0(L_2, ((float)il2cpp_codegen_multiply(L_3, L_4)), NULL);
	}

IL_0025:
	{
		// }
		return;
	}
}
// System.Void QFramework.AudioPlayer::OnResLoadFinish(System.Boolean,UnityEngine.AudioClip)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_OnResLoadFinish_m62EB8A101B47B4E722D75F817C37ED012B1A33B4 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, bool ___0_result, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___1_clip, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteralA3D636F286CFC52E32577AAF6E182EB8B0FDAAA3);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (!result)
		bool L_0 = ___0_result;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		// Release();
		AudioPlayer_Release_mD740E38F2E434B0B6F98C8F93F19F498D6318595(__this, NULL);
		// return;
		return;
	}

IL_000a:
	{
		// mAudioClip = clip;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_1 = ___1_clip;
		__this->___mAudioClip_4 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mAudioClip_4), (void*)L_1);
		// if (mAudioClip == null)
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_2 = __this->___mAudioClip_4;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_3;
		L_3 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_2, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_3)
		{
			goto IL_003b;
		}
	}
	{
		// Debug.LogError("Asset Is Invalid AudioClip:" + mName);
		String_t* L_4 = __this->___mName_2;
		String_t* L_5;
		L_5 = String_Concat_m9E3155FB84015C823606188F53B47CB44C444991(_stringLiteralA3D636F286CFC52E32577AAF6E182EB8B0FDAAA3, L_4, NULL);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogError_mB00B2B4468EF3CAF041B038D840820FB84C924B2(L_5, NULL);
		// Release();
		AudioPlayer_Release_mD740E38F2E434B0B6F98C8F93F19F498D6318595(__this, NULL);
		// return;
		return;
	}

IL_003b:
	{
		// PlayAudioClip();
		AudioPlayer_PlayAudioClip_mE75B2E82ED39EC2C929277F8342A2749FFDE80F6(__this, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioPlayer::PlayAudioClip()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_PlayAudioClip_mE75B2E82ED39EC2C929277F8342A2749FFDE80F6 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioPlayer_OnSoundPlayFinish_mEED80DC657DE5D9004B528A3E174D045EE01BDED_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B7_0 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B6_0 = NULL;
	{
		// if (mAudioSource == null || mAudioClip == null)
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_0 = __this->___mAudioSource_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_0, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (L_1)
		{
			goto IL_001c;
		}
	}
	{
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_2 = __this->___mAudioClip_4;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_3;
		L_3 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_2, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_3)
		{
			goto IL_0023;
		}
	}

IL_001c:
	{
		// Release();
		AudioPlayer_Release_mD740E38F2E434B0B6F98C8F93F19F498D6318595(__this, NULL);
		// return;
		return;
	}

IL_0023:
	{
		// mAudioSource.clip = mAudioClip;
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_4 = __this->___mAudioSource_1;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_5 = __this->___mAudioClip_4;
		NullCheck(L_4);
		AudioSource_set_clip_mFF441895E274286C88D9C75ED5CA1B1B39528D70(L_4, L_5, NULL);
		// mAudioSource.loop = IsLoop;
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_6 = __this->___mAudioSource_1;
		bool L_7;
		L_7 = AudioPlayer_get_IsLoop_mC43549D534C455179A16FF1C2EDA241B117717E7_inline(__this, NULL);
		NullCheck(L_6);
		AudioSource_set_loop_m834A590939D8456008C0F897FD80B0ECFFB7FE56(L_6, L_7, NULL);
		// UpdateVolume();
		AudioPlayer_UpdateVolume_m2D4666F4768B21FEF1D9B2AE1E34063F343552A9(__this, NULL);
		// int loopCount = 1;
		V_0 = 1;
		// if (IsLoop)
		bool L_8;
		L_8 = AudioPlayer_get_IsLoop_mC43549D534C455179A16FF1C2EDA241B117717E7_inline(__this, NULL);
		if (!L_8)
		{
			goto IL_0057;
		}
	}
	{
		// loopCount = -1;
		V_0 = (-1);
	}

IL_0057:
	{
		// mTimeItem = Timer.Instance.Post2Scale(OnSoundPlayFinish, mAudioClip.length, loopCount);
		Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* L_9;
		L_9 = Timer_get_Instance_m20E57C957705CF1168B025EEAB98FAF69631694D(NULL);
		Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* L_10 = (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404*)il2cpp_codegen_object_new(Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404_il2cpp_TypeInfo_var);
		NullCheck(L_10);
		Action_1__ctor_m1BA854F3F4319EA4A4294DDFDA21C395B8D0FF87(L_10, __this, (intptr_t)((void*)AudioPlayer_OnSoundPlayFinish_mEED80DC657DE5D9004B528A3E174D045EE01BDED_RuntimeMethod_var), NULL);
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_11 = __this->___mAudioClip_4;
		NullCheck(L_11);
		float L_12;
		L_12 = AudioClip_get_length_m6102CB29AF65988797452E4D6E43D4788303873D(L_11, NULL);
		int32_t L_13 = V_0;
		NullCheck(L_9);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_14;
		L_14 = Timer_Post2Scale_m58F7D7517DF78356B41793A5FF4F930306E0117E(L_9, L_10, L_12, L_13, NULL);
		__this->___mTimeItem_5 = L_14;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mTimeItem_5), (void*)L_14);
		// mOnStart?.Invoke();
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_15 = __this->___mOnStart_6;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_16 = L_15;
		G_B6_0 = L_16;
		if (L_16)
		{
			G_B7_0 = L_16;
			goto IL_008b;
		}
	}
	{
		goto IL_0090;
	}

IL_008b:
	{
		NullCheck(G_B7_0);
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(G_B7_0, NULL);
	}

IL_0090:
	{
		// mOnStart = null;
		__this->___mOnStart_6 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mOnStart_6), (void*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL);
		// mAudioSource.Play();
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_17 = __this->___mAudioSource_1;
		NullCheck(L_17);
		AudioSource_Play_m95DF07111C61D0E0F00257A00384D31531D590C3(L_17, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioPlayer::OnResumeTimeTick(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_OnResumeTimeTick_m82DE3AC83904EE1C80B30D28C5B00BC6CBEA04B2 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, int32_t ___0_repeatCount, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioPlayer_OnSoundPlayFinish_mEED80DC657DE5D9004B528A3E174D045EE01BDED_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// OnSoundPlayFinish(repeatCount);
		int32_t L_0 = ___0_repeatCount;
		AudioPlayer_OnSoundPlayFinish_mEED80DC657DE5D9004B528A3E174D045EE01BDED(__this, L_0, NULL);
		// if (IsLoop)
		bool L_1;
		L_1 = AudioPlayer_get_IsLoop_mC43549D534C455179A16FF1C2EDA241B117717E7_inline(__this, NULL);
		if (!L_1)
		{
			goto IL_0037;
		}
	}
	{
		// mTimeItem = Timer.Instance.Post2Scale(OnSoundPlayFinish, mAudioClip.length, -1);
		Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* L_2;
		L_2 = Timer_get_Instance_m20E57C957705CF1168B025EEAB98FAF69631694D(NULL);
		Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* L_3 = (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404*)il2cpp_codegen_object_new(Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404_il2cpp_TypeInfo_var);
		NullCheck(L_3);
		Action_1__ctor_m1BA854F3F4319EA4A4294DDFDA21C395B8D0FF87(L_3, __this, (intptr_t)((void*)AudioPlayer_OnSoundPlayFinish_mEED80DC657DE5D9004B528A3E174D045EE01BDED_RuntimeMethod_var), NULL);
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_4 = __this->___mAudioClip_4;
		NullCheck(L_4);
		float L_5;
		L_5 = AudioClip_get_length_m6102CB29AF65988797452E4D6E43D4788303873D(L_4, NULL);
		NullCheck(L_2);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_6;
		L_6 = Timer_Post2Scale_m58F7D7517DF78356B41793A5FF4F930306E0117E(L_2, L_3, L_5, (-1), NULL);
		__this->___mTimeItem_5 = L_6;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mTimeItem_5), (void*)L_6);
	}

IL_0037:
	{
		// }
		return;
	}
}
// System.Void QFramework.AudioPlayer::OnSoundPlayFinish(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_OnSoundPlayFinish_mEED80DC657DE5D9004B528A3E174D045EE01BDED (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, int32_t ___0_count, const RuntimeMethod* method) 
{
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B2_0 = NULL;
	Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* G_B1_0 = NULL;
	{
		// mOnFinish?.Invoke();
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_0 = __this->___mOnFinish_7;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000c;
		}
	}
	{
		goto IL_0011;
	}

IL_000c:
	{
		NullCheck(G_B2_0);
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(G_B2_0, NULL);
	}

IL_0011:
	{
		// mOnFinish = null;
		__this->___mOnFinish_7 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mOnFinish_7), (void*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL);
		// if (!IsLoop)
		bool L_2;
		L_2 = AudioPlayer_get_IsLoop_mC43549D534C455179A16FF1C2EDA241B117717E7_inline(__this, NULL);
		if (L_2)
		{
			goto IL_0026;
		}
	}
	{
		// Release();
		AudioPlayer_Release_mD740E38F2E434B0B6F98C8F93F19F498D6318595(__this, NULL);
	}

IL_0026:
	{
		// }
		return;
	}
}
// System.Void QFramework.AudioPlayer::Release()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_Release_mD740E38F2E434B0B6F98C8F93F19F498D6318595 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// CleanResources();
		AudioPlayer_CleanResources_mC72F4BF993C81EADE75AA24B40E3D39A3ACB4E63(__this, NULL);
		// if (UsedCache)
		bool L_0;
		L_0 = AudioPlayer_get_UsedCache_m90781A61EB4E15DE4B973135C4D973D661351885_inline(__this, NULL);
		if (!L_0)
		{
			goto IL_0014;
		}
	}
	{
		// Recycle2Cache();
		AudioPlayer_Recycle2Cache_mF99CD679BAF547DA43B1BA246824B9171E1EFBF5(__this, NULL);
	}

IL_0014:
	{
		// }
		return;
	}
}
// System.Void QFramework.AudioPlayer::CleanResources()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_CleanResources_mC72F4BF993C81EADE75AA24B40E3D39A3ACB4E63 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IAudioLoaderPool_t25A31F4FC2301F1B46FC33A0BF0C2E1F65589A22_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IAudioLoader_t02005AF5330B4CD269CC97AB97F7A9510BE4D79A_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// mName = null;
		__this->___mName_2 = (String_t*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mName_2), (void*)(String_t*)NULL);
		// mIsPause = false;
		__this->___mIsPause_8 = (bool)0;
		// mLeftDelayTime = -1;
		__this->___mLeftDelayTime_9 = (-1.0f);
		// if (mTimeItem != null)
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_0 = __this->___mTimeItem_5;
		if (!L_0)
		{
			goto IL_0033;
		}
	}
	{
		// mTimeItem.Cancel();
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_1 = __this->___mTimeItem_5;
		NullCheck(L_1);
		TimeItem_Cancel_m37972167491A71932F518C5FECA2F73EE17CB62B(L_1, NULL);
		// mTimeItem = null;
		__this->___mTimeItem_5 = (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mTimeItem_5), (void*)(TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8*)NULL);
	}

IL_0033:
	{
		// if (mAudioSource)
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_2 = __this->___mAudioSource_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_3;
		L_3 = Object_op_Implicit_m93896EF7D68FA113C42D3FE2BC6F661FC7EF514A(L_2, NULL);
		if (!L_3)
		{
			goto IL_006f;
		}
	}
	{
		// if (mAudioSource.clip == mAudioClip)
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_4 = __this->___mAudioSource_1;
		NullCheck(L_4);
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_5;
		L_5 = AudioSource_get_clip_m4F5027066F9FC44B44192713142B0C277BB418FE(L_4, NULL);
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_6 = __this->___mAudioClip_4;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_7;
		L_7 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605(L_5, L_6, NULL);
		if (!L_7)
		{
			goto IL_006f;
		}
	}
	{
		// mAudioSource.Stop();
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_8 = __this->___mAudioSource_1;
		NullCheck(L_8);
		AudioSource_Stop_m318F17F17A147C77FF6E0A5A7A6BE057DB90F537(L_8, NULL);
		// mAudioSource.clip = null;
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_9 = __this->___mAudioSource_1;
		NullCheck(L_9);
		AudioSource_set_clip_mFF441895E274286C88D9C75ED5CA1B1B39528D70(L_9, (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*)NULL, NULL);
	}

IL_006f:
	{
		// mAudioClip = null;
		__this->___mAudioClip_4 = (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mAudioClip_4), (void*)(AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*)NULL);
		// if (mLoader != null)
		RuntimeObject* L_10 = __this->___mLoader_0;
		if (!L_10)
		{
			goto IL_00a5;
		}
	}
	{
		// mLoader.Unload();
		RuntimeObject* L_11 = __this->___mLoader_0;
		NullCheck(L_11);
		InterfaceActionInvoker0::Invoke(3 /* System.Void QFramework.IAudioLoader::Unload() */, IAudioLoader_t02005AF5330B4CD269CC97AB97F7A9510BE4D79A_il2cpp_TypeInfo_var, L_11);
		// AudioKit.Config.AudioLoaderPool.RecycleLoader(mLoader);
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKitConfig_t0518724CE4F14A672627665BE74D533751686A54* L_12 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___Config_0;
		NullCheck(L_12);
		RuntimeObject* L_13 = L_12->___AudioLoaderPool_0;
		RuntimeObject* L_14 = __this->___mLoader_0;
		NullCheck(L_13);
		InterfaceActionInvoker1< RuntimeObject* >::Invoke(1 /* System.Void QFramework.IAudioLoaderPool::RecycleLoader(QFramework.IAudioLoader) */, IAudioLoaderPool_t25A31F4FC2301F1B46FC33A0BF0C2E1F65589A22_il2cpp_TypeInfo_var, L_13, L_14);
		// mLoader = null;
		__this->___mLoader_0 = (RuntimeObject*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mLoader_0), (void*)(RuntimeObject*)NULL);
	}

IL_00a5:
	{
		// }
		return;
	}
}
// System.Void QFramework.AudioPlayer::OnRecycled()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_OnRecycled_mD09A82E6CD4BDFED9D6797D7E5FD8097855E62D1 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioPlayer_SetVolume_m0306F9423FAFF5F273E941A297F11EC77D516260_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BindableProperty_1_UnRegister_mDE5848741098D28A95EE63902771C3214DB883CA_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* G_B2_0 = NULL;
	BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* G_B1_0 = NULL;
	{
		// Volume?.UnRegister(SetVolume);
		BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* L_0;
		L_0 = AudioPlayer_get_Volume_m2095A2461F76393AA590905BEB51E7E90A174126_inline(__this, NULL);
		BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* L_1 = L_0;
		G_B1_0 = L_1;
		if (L_1)
		{
			G_B2_0 = L_1;
			goto IL_000c;
		}
	}
	{
		goto IL_001d;
	}

IL_000c:
	{
		Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* L_2 = (Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A*)il2cpp_codegen_object_new(Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A_il2cpp_TypeInfo_var);
		NullCheck(L_2);
		Action_1__ctor_m770CD2F8BB65F2EDA5128CA2F96D71C35B23E859(L_2, __this, (intptr_t)((void*)AudioPlayer_SetVolume_m0306F9423FAFF5F273E941A297F11EC77D516260_RuntimeMethod_var), NULL);
		NullCheck(G_B2_0);
		BindableProperty_1_UnRegister_mDE5848741098D28A95EE63902771C3214DB883CA(G_B2_0, L_2, BindableProperty_1_UnRegister_mDE5848741098D28A95EE63902771C3214DB883CA_RuntimeMethod_var);
	}

IL_001d:
	{
		// Volume = null;
		AudioPlayer_set_Volume_m8D35738EFB189D11C6FBFA3A1C116B3C4D007F5E_inline(__this, (BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58*)NULL, NULL);
		// CleanResources();
		AudioPlayer_CleanResources_mC72F4BF993C81EADE75AA24B40E3D39A3ACB4E63(__this, NULL);
		// }
		return;
	}
}
// System.Void QFramework.AudioPlayer::Recycle2Cache()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer_Recycle2Cache_mF99CD679BAF547DA43B1BA246824B9171E1EFBF5 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (!SafeObjectPool<AudioPlayer>.Instance.Recycle(this))
		SafeObjectPool_1_t3542EC4AA4871EFF26264DA85DCDB6C39D22A8AD* L_0;
		L_0 = SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421(SafeObjectPool_1_get_Instance_mE75F04B4FDCB3998ED642AF2F1C46499E9E5B421_RuntimeMethod_var);
		NullCheck(L_0);
		bool L_1;
		L_1 = VirtualFuncInvoker1< bool, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* >::Invoke(7 /* System.Boolean QFramework.Pool`1<QFramework.AudioPlayer>::Recycle(T) */, L_0, __this);
		if (L_1)
		{
			goto IL_002d;
		}
	}
	{
		// if (mAudioSource != null)
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_2 = __this->___mAudioSource_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_3;
		L_3 = Object_op_Inequality_mD0BE578448EAA61948F25C32F8DD55AB1F778602(L_2, (Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, NULL);
		if (!L_3)
		{
			goto IL_002d;
		}
	}
	{
		// Object.Destroy(mAudioSource);
		AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299* L_4 = __this->___mAudioSource_1;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		Object_Destroy_mE97D0A766419A81296E8D4E5C23D01D3FE91ACBB(L_4, NULL);
		// mAudioSource = null;
		__this->___mAudioSource_1 = (AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mAudioSource_1), (void*)(AudioSource_t871AC2272F896738252F04EE949AEF5B241D3299*)NULL);
	}

IL_002d:
	{
		// }
		return;
	}
}
// System.Void QFramework.AudioPlayer::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioPlayer__ctor_mBFD36A1EFC8832607AD14E90532A3492F8656E72 (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// private float mLeftDelayTime = -1;
		__this->___mLeftDelayTime_9 = (-1.0f);
		// private float mVolumeScale = 1.0f;
		__this->___mVolumeScale_10 = (1.0f);
		// public bool UsedCache { get; set; } = true;
		__this->___U3CUsedCacheU3Ek__BackingField_12 = (bool)1;
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
// QFramework.FluentMusicAPI QFramework.FluentMusicAPI::Allocate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* FluentMusicAPI_Allocate_m47EA60F843D95341E109C9D8FDDC8436B003579E (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SafeObjectPool_1_get_Instance_m3795D49BEBB3B1FBDA78876A916F253627F999D5_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// return SafeObjectPool<FluentMusicAPI>.Instance.Allocate();
		SafeObjectPool_1_t33C1137434D07D7A43A1A8C7D46F1C04B665E6F0* L_0;
		L_0 = SafeObjectPool_1_get_Instance_m3795D49BEBB3B1FBDA78876A916F253627F999D5(SafeObjectPool_1_get_Instance_m3795D49BEBB3B1FBDA78876A916F253627F999D5_RuntimeMethod_var);
		NullCheck(L_0);
		FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* L_1;
		L_1 = VirtualFuncInvoker0< FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* >::Invoke(6 /* T QFramework.Pool`1<QFramework.FluentMusicAPI>::Allocate() */, L_0);
		return L_1;
	}
}
// System.Void QFramework.FluentMusicAPI::OnRecycled()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FluentMusicAPI_OnRecycled_m64FBF876CF2C74173D9DCCB02B9F52F7095A1DB6 (FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* __this, const RuntimeMethod* method) 
{
	{
		// mName = null;
		__this->___mName_1 = (String_t*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mName_1), (void*)(String_t*)NULL);
		// mClip = null;
		__this->___mClip_2 = (AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mClip_2), (void*)(AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20*)NULL);
		// mLoop = true;
		__this->___mLoop_3 = (bool)1;
		// mVolumeScale = 1;
		__this->___mVolumeScale_4 = (1.0f);
		// }
		return;
	}
}
// System.Boolean QFramework.FluentMusicAPI::get_IsRecycled()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool FluentMusicAPI_get_IsRecycled_m9B148E222973C4E2D465BF91F150304A4C355B8F (FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsRecycled { get; set; }
		bool L_0 = __this->___U3CIsRecycledU3Ek__BackingField_0;
		return L_0;
	}
}
// System.Void QFramework.FluentMusicAPI::set_IsRecycled(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FluentMusicAPI_set_IsRecycled_m5089C5681A2F881999FD3F04FA5389E016ED6A5A (FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		// public bool IsRecycled { get; set; }
		bool L_0 = ___0_value;
		__this->___U3CIsRecycledU3Ek__BackingField_0 = L_0;
		return;
	}
}
// System.Void QFramework.FluentMusicAPI::Recycle2Cache()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FluentMusicAPI_Recycle2Cache_m2F554C716630F94B7C2DB934BF598FD47EC8167C (FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SafeObjectPool_1_get_Instance_m3795D49BEBB3B1FBDA78876A916F253627F999D5_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// SafeObjectPool<FluentMusicAPI>.Instance.Recycle(this);
		SafeObjectPool_1_t33C1137434D07D7A43A1A8C7D46F1C04B665E6F0* L_0;
		L_0 = SafeObjectPool_1_get_Instance_m3795D49BEBB3B1FBDA78876A916F253627F999D5(SafeObjectPool_1_get_Instance_m3795D49BEBB3B1FBDA78876A916F253627F999D5_RuntimeMethod_var);
		NullCheck(L_0);
		bool L_1;
		L_1 = VirtualFuncInvoker1< bool, FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* >::Invoke(7 /* System.Boolean QFramework.Pool`1<QFramework.FluentMusicAPI>::Recycle(T) */, L_0, __this);
		// }
		return;
	}
}
// QFramework.FluentMusicAPI QFramework.FluentMusicAPI::WithName(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* FluentMusicAPI_WithName_m397642E59983936656A7E1BC8E67E705E3684B70 (FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* __this, String_t* ___0_name, const RuntimeMethod* method) 
{
	{
		// mName = name;
		String_t* L_0 = ___0_name;
		__this->___mName_1 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mName_1), (void*)L_0);
		// return this;
		return __this;
	}
}
// QFramework.FluentMusicAPI QFramework.FluentMusicAPI::WithAudioClip(UnityEngine.AudioClip)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* FluentMusicAPI_WithAudioClip_m5998496892F05D87D36F42D5807BB0C5F2AA916A (FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* __this, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, const RuntimeMethod* method) 
{
	{
		// mClip = clip;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_0 = ___0_clip;
		__this->___mClip_2 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mClip_2), (void*)L_0);
		// return this;
		return __this;
	}
}
// QFramework.FluentMusicAPI QFramework.FluentMusicAPI::Loop(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* FluentMusicAPI_Loop_mD16395730310B6BBA02BA190AAE4F207772D1BF6 (FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* __this, bool ___0_loop, const RuntimeMethod* method) 
{
	{
		// mLoop = loop;
		bool L_0 = ___0_loop;
		__this->___mLoop_3 = L_0;
		// return this;
		return __this;
	}
}
// QFramework.FluentMusicAPI QFramework.FluentMusicAPI::VolumeScale(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* FluentMusicAPI_VolumeScale_mFC8736330C21562A62E3D7D445332182C0E40979 (FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* __this, float ___0_volumeScale, const RuntimeMethod* method) 
{
	{
		// mVolumeScale = volumeScale;
		float L_0 = ___0_volumeScale;
		__this->___mVolumeScale_4 = L_0;
		// return this;
		return __this;
	}
}
// System.Void QFramework.FluentMusicAPI::Play()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FluentMusicAPI_Play_m622C6D34F3635F64EBDF9A168D5E4C60F0758EA2 (FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (mName != null)
		String_t* L_0 = __this->___mName_1;
		if (!L_0)
		{
			goto IL_002e;
		}
	}
	{
		// AudioKit.PlayMusic(mName, mLoop, onEndCallback: Recycle2Cache, volume: mVolumeScale);
		String_t* L_1 = __this->___mName_1;
		bool L_2 = __this->___mLoop_3;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_3 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		NullCheck(L_3);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_3, __this, (intptr_t)((void*)GetVirtualMethodInfo(__this, 7)), NULL);
		float L_4 = __this->___mVolumeScale_4;
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKit_PlayMusic_m024F70A37392A7DB77FD290AAA197DCF0FA7B8F6(L_1, L_2, (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL, L_3, L_4, NULL);
		return;
	}

IL_002e:
	{
		// AudioKit.PlayMusic(mClip, mLoop, onEndCallback: Recycle2Cache, volume: mVolumeScale);
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_5 = __this->___mClip_2;
		bool L_6 = __this->___mLoop_3;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_7 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)il2cpp_codegen_object_new(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07_il2cpp_TypeInfo_var);
		NullCheck(L_7);
		Action__ctor_mBDC7B0B4A3F583B64C2896F01BDED360772F67DC(L_7, __this, (intptr_t)((void*)GetVirtualMethodInfo(__this, 7)), NULL);
		float L_8 = __this->___mVolumeScale_4;
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKit_PlayMusic_mA8FCD6EF87F4407C53D72C88A43A559C6440526B(L_5, L_6, (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL, L_7, L_8, NULL);
		// }
		return;
	}
}
// System.Void QFramework.FluentMusicAPI::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FluentMusicAPI__ctor_mCF7C6585D6A2127DFF434C648686D8509A06D752 (FluentMusicAPI_t7460A6D852557366A9A2D1DC68889B400D69485A* __this, const RuntimeMethod* method) 
{
	{
		// private bool mLoop = true;
		__this->___mLoop_3 = (bool)1;
		// private float mVolumeScale = 1;
		__this->___mVolumeScale_4 = (1.0f);
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
// QFramework.FluentSoundAPI QFramework.FluentSoundAPI::Allocate()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* FluentSoundAPI_Allocate_mCB696368A37809648CBB30613D38F8CE94DF18A0 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SafeObjectPool_1_get_Instance_m462FA7E647B9136571534928DB536BDFE6AB8B86_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static FluentSoundAPI Allocate() => SafeObjectPool<FluentSoundAPI>.Instance.Allocate();
		SafeObjectPool_1_t7FA207C564A34D5D5E4DE789675614FD0D0964A2* L_0;
		L_0 = SafeObjectPool_1_get_Instance_m462FA7E647B9136571534928DB536BDFE6AB8B86(SafeObjectPool_1_get_Instance_m462FA7E647B9136571534928DB536BDFE6AB8B86_RuntimeMethod_var);
		NullCheck(L_0);
		FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* L_1;
		L_1 = VirtualFuncInvoker0< FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* >::Invoke(6 /* T QFramework.Pool`1<QFramework.FluentSoundAPI>::Allocate() */, L_0);
		return L_1;
	}
}
// System.Void QFramework.FluentSoundAPI::OnRecycled()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FluentSoundAPI_OnRecycled_m19946E973500129C854D22C1F2854606E77F1664 (FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* __this, const RuntimeMethod* method) 
{
	{
		// mName = null;
		__this->___mName_1 = (String_t*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mName_1), (void*)(String_t*)NULL);
		// mLoop = false;
		__this->___mLoop_3 = (bool)0;
		// }
		return;
	}
}
// System.Boolean QFramework.FluentSoundAPI::get_IsRecycled()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool FluentSoundAPI_get_IsRecycled_m5D883D63976CDC2BA49DA833C6A9A687B954F447 (FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsRecycled { get; set; }
		bool L_0 = __this->___U3CIsRecycledU3Ek__BackingField_0;
		return L_0;
	}
}
// System.Void QFramework.FluentSoundAPI::set_IsRecycled(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FluentSoundAPI_set_IsRecycled_m1DE80FC0179FAFB11C1E6C9F6402AFB164122ADC (FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		// public bool IsRecycled { get; set; }
		bool L_0 = ___0_value;
		__this->___U3CIsRecycledU3Ek__BackingField_0 = L_0;
		return;
	}
}
// System.Void QFramework.FluentSoundAPI::Recycle2Cache()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FluentSoundAPI_Recycle2Cache_m99C6296EADD977F1C32FA60148A22A3F8312DF68 (FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SafeObjectPool_1_get_Instance_m462FA7E647B9136571534928DB536BDFE6AB8B86_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// SafeObjectPool<FluentSoundAPI>.Instance.Recycle(this);
		SafeObjectPool_1_t7FA207C564A34D5D5E4DE789675614FD0D0964A2* L_0;
		L_0 = SafeObjectPool_1_get_Instance_m462FA7E647B9136571534928DB536BDFE6AB8B86(SafeObjectPool_1_get_Instance_m462FA7E647B9136571534928DB536BDFE6AB8B86_RuntimeMethod_var);
		NullCheck(L_0);
		bool L_1;
		L_1 = VirtualFuncInvoker1< bool, FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* >::Invoke(7 /* System.Boolean QFramework.Pool`1<QFramework.FluentSoundAPI>::Recycle(T) */, L_0, __this);
		// }
		return;
	}
}
// QFramework.FluentSoundAPI QFramework.FluentSoundAPI::WithName(System.String)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* FluentSoundAPI_WithName_m05BD4330B3A520720B96A85A0F27B0B080D73259 (FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* __this, String_t* ___0_name, const RuntimeMethod* method) 
{
	{
		// mName = name;
		String_t* L_0 = ___0_name;
		__this->___mName_1 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mName_1), (void*)L_0);
		// return this;
		return __this;
	}
}
// QFramework.FluentSoundAPI QFramework.FluentSoundAPI::WithAudioClip(UnityEngine.AudioClip)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* FluentSoundAPI_WithAudioClip_m3487F2D1DACBC9BC95AC29F43CC441A2E24C9C0A (FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* __this, AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* ___0_clip, const RuntimeMethod* method) 
{
	{
		// mClip = clip;
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_0 = ___0_clip;
		__this->___mClip_2 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mClip_2), (void*)L_0);
		// return this;
		return __this;
	}
}
// QFramework.FluentSoundAPI QFramework.FluentSoundAPI::Loop(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* FluentSoundAPI_Loop_m1C939802C4530284DABECB6082FF70F6FF33F344 (FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* __this, bool ___0_loop, const RuntimeMethod* method) 
{
	{
		// mLoop = loop;
		bool L_0 = ___0_loop;
		__this->___mLoop_3 = L_0;
		// return this;
		return __this;
	}
}
// QFramework.FluentSoundAPI QFramework.FluentSoundAPI::VolumeScale(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* FluentSoundAPI_VolumeScale_mE13842D75C89122FFFBAFDB6FA7A62C4C7AD727D (FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* __this, float ___0_volumeScale, const RuntimeMethod* method) 
{
	{
		// mVolumeScale = volumeScale;
		float L_0 = ___0_volumeScale;
		__this->___mVolumeScale_4 = L_0;
		// return this;
		return __this;
	}
}
// QFramework.AudioPlayer QFramework.FluentSoundAPI::Play()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* FluentSoundAPI_Play_m6ACFF63E5856F6F703C5984A2FBF5030AA467583 (FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&FluentSoundAPI_U3CPlayU3Eb__15_0_m3D13802E1118D4232F92DA68DFB998D43FD35A95_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&FluentSoundAPI_U3CPlayU3Eb__15_1_m92225A92EF2E9F135347E99E564002BADAF775BD_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* V_0 = NULL;
	{
		// AudioPlayer soundPlayer = null;
		V_0 = (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59*)NULL;
		// if (mName != null)
		String_t* L_0 = __this->___mName_1;
		if (!L_0)
		{
			goto IL_0030;
		}
	}
	{
		// soundPlayer =  AudioKit.PlaySound(mName, mLoop, callBack: (p) =>
		// {
		//     Recycle2Cache();
		// }, volume: mVolumeScale);
		String_t* L_1 = __this->___mName_1;
		bool L_2 = __this->___mLoop_3;
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_3 = (Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219*)il2cpp_codegen_object_new(Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219_il2cpp_TypeInfo_var);
		NullCheck(L_3);
		Action_1__ctor_m3C560AF46A6955D28CF1A054DD88FD769247FDF0(L_3, __this, (intptr_t)((void*)FluentSoundAPI_U3CPlayU3Eb__15_0_m3D13802E1118D4232F92DA68DFB998D43FD35A95_RuntimeMethod_var), NULL);
		float L_4 = __this->___mVolumeScale_4;
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_5;
		L_5 = AudioKit_PlaySound_m9D36192E4F9A94A43432AE08EAB4852B703F6D29(L_1, L_2, L_3, L_4, NULL);
		V_0 = L_5;
		goto IL_0054;
	}

IL_0030:
	{
		// soundPlayer = AudioKit.PlaySound(mClip, mLoop, callBack: (p) =>
		// {
		//     Recycle2Cache();
		// }, volume: mVolumeScale);
		AudioClip_t5D272C4EB4F2D3ED49F1C346DEA373CF6D585F20* L_6 = __this->___mClip_2;
		bool L_7 = __this->___mLoop_3;
		Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219* L_8 = (Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219*)il2cpp_codegen_object_new(Action_1_tC1BDD318F67985AA1EED29272C1E98816DF76219_il2cpp_TypeInfo_var);
		NullCheck(L_8);
		Action_1__ctor_m3C560AF46A6955D28CF1A054DD88FD769247FDF0(L_8, __this, (intptr_t)((void*)FluentSoundAPI_U3CPlayU3Eb__15_1_m92225A92EF2E9F135347E99E564002BADAF775BD_RuntimeMethod_var), NULL);
		float L_9 = __this->___mVolumeScale_4;
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_10;
		L_10 = AudioKit_PlaySound_m38FDA779D76D117AB6E58EA321E2DF0F1FA9B1E1(L_6, L_7, L_8, L_9, NULL);
		V_0 = L_10;
	}

IL_0054:
	{
		// if (soundPlayer == null)
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_11 = V_0;
		if (L_11)
		{
			goto IL_005d;
		}
	}
	{
		// Recycle2Cache();
		FluentSoundAPI_Recycle2Cache_m99C6296EADD977F1C32FA60148A22A3F8312DF68(__this, NULL);
	}

IL_005d:
	{
		// return soundPlayer;
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_12 = V_0;
		return L_12;
	}
}
// System.Void QFramework.FluentSoundAPI::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FluentSoundAPI__ctor_m49824EAB3A7155DA3E695ED0D1F55735C37CA392 (FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* __this, const RuntimeMethod* method) 
{
	{
		// private float mVolumeScale = 1;
		__this->___mVolumeScale_4 = (1.0f);
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Void QFramework.FluentSoundAPI::<Play>b__15_0(QFramework.AudioPlayer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FluentSoundAPI_U3CPlayU3Eb__15_0_m3D13802E1118D4232F92DA68DFB998D43FD35A95 (FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_p, const RuntimeMethod* method) 
{
	{
		// Recycle2Cache();
		FluentSoundAPI_Recycle2Cache_m99C6296EADD977F1C32FA60148A22A3F8312DF68(__this, NULL);
		// }, volume: mVolumeScale);
		return;
	}
}
// System.Void QFramework.FluentSoundAPI::<Play>b__15_1(QFramework.AudioPlayer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void FluentSoundAPI_U3CPlayU3Eb__15_1_m92225A92EF2E9F135347E99E564002BADAF775BD (FluentSoundAPI_tD408210D2CDB2D54D5BB45014A5D57B205D7E97C* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_p, const RuntimeMethod* method) 
{
	{
		// Recycle2Cache();
		FluentSoundAPI_Recycle2Cache_m99C6296EADD977F1C32FA60148A22A3F8312DF68(__this, NULL);
		// }, volume: mVolumeScale);
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
// System.Void QFramework.AudioKitOnFinishExtensions::OnFinish(QFramework.IAudioKitOnFinish,System.Action)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void AudioKitOnFinishExtensions_OnFinish_mC950466C398349874B4E4EC22F908C665E7A739A (RuntimeObject* ___0_self, Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* ___1_onFinish, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&IAudioKitOnFinish_tF0FC1CC3932C215418825A77BF074D98BA83E79C_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// self?.OnFinish(onFinish);
		RuntimeObject* L_0 = ___0_self;
		if (!L_0)
		{
			goto IL_000a;
		}
	}
	{
		RuntimeObject* L_1 = ___0_self;
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_2 = ___1_onFinish;
		NullCheck(L_1);
		InterfaceActionInvoker1< Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* >::Invoke(0 /* System.Void QFramework.IAudioKitOnFinish::OnFinish(System.Action) */, IAudioKitOnFinish_tF0FC1CC3932C215418825A77BF074D98BA83E79C_il2cpp_TypeInfo_var, L_1, L_2);
	}

IL_000a:
	{
		// }
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
// System.Int32 QFramework.IntProperty::get_Value()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t IntProperty_get_Value_mC001864FF517D3D3104A9E6B8EDCA9D3C54FE60F (IntProperty_t4E5B676427BABBB583A5A88D57DA2C0BCF45A10A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Property_1_get_Value_mAB5065F7B5D8CB3B340114A5BB2953765536E0C7_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// get { return base.Value; }
		int32_t L_0;
		L_0 = Property_1_get_Value_mAB5065F7B5D8CB3B340114A5BB2953765536E0C7(__this, Property_1_get_Value_mAB5065F7B5D8CB3B340114A5BB2953765536E0C7_RuntimeMethod_var);
		return L_0;
	}
}
// System.Void QFramework.IntProperty::set_Value(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void IntProperty_set_Value_mC35BC602DDFCE1C5F8A303D09FB4CDF82E6034DF (IntProperty_t4E5B676427BABBB583A5A88D57DA2C0BCF45A10A* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Property_1_set_Value_m314C5C5A72F5F0400ABFB00DFC1466A3434D26F3_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// set { base.Value = value; }
		int32_t L_0 = ___0_value;
		Property_1_set_Value_m314C5C5A72F5F0400ABFB00DFC1466A3434D26F3(__this, L_0, Property_1_set_Value_m314C5C5A72F5F0400ABFB00DFC1466A3434D26F3_RuntimeMethod_var);
		// set { base.Value = value; }
		return;
	}
}
// System.Void QFramework.IntProperty::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void IntProperty__ctor_m5606F95D322A5A2FC590E4412EC7FD4B324A006C (IntProperty_t4E5B676427BABBB583A5A88D57DA2C0BCF45A10A* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Property_1__ctor_m77CDCAA18D9FC2F380162C19E268B4B1956628E8_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		Property_1__ctor_m77CDCAA18D9FC2F380162C19E268B4B1956628E8(__this, Property_1__ctor_m77CDCAA18D9FC2F380162C19E268B4B1956628E8_RuntimeMethod_var);
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
// QFramework.TimeItem QFramework.TimeItem::Allocate(System.Action`1<System.Int32>,System.Single,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* TimeItem_Allocate_m8D72EDD32571CF80232A79522386B079D75B30D3 (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* ___0_callback, float ___1_delayTime, int32_t ___2_repeatCount, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SafeObjectPool_1_get_Instance_m27DE656011C461C719BEA3A05D455044BCA2757A_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// TimeItem item = SafeObjectPool<TimeItem>.Instance.Allocate();
		SafeObjectPool_1_tB9A3A3095B574142CF0139B7323BA66C80DCC342* L_0;
		L_0 = SafeObjectPool_1_get_Instance_m27DE656011C461C719BEA3A05D455044BCA2757A(SafeObjectPool_1_get_Instance_m27DE656011C461C719BEA3A05D455044BCA2757A_RuntimeMethod_var);
		NullCheck(L_0);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_1;
		L_1 = VirtualFuncInvoker0< TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* >::Invoke(6 /* T QFramework.Pool`1<QFramework.TimeItem>::Allocate() */, L_0);
		// item.Set(callback, delayTime, repeatCount);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_2 = L_1;
		Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* L_3 = ___0_callback;
		float L_4 = ___1_delayTime;
		int32_t L_5 = ___2_repeatCount;
		NullCheck(L_2);
		TimeItem_Set_m4FD3CF9D2583C7630D8B5CF5BA80C8C43DC31154(L_2, L_3, L_4, L_5, NULL);
		// return item;
		return L_2;
	}
}
// System.Void QFramework.TimeItem::Set(System.Action`1<System.Int32>,System.Single,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem_Set_m4FD3CF9D2583C7630D8B5CF5BA80C8C43DC31154 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* ___0_callback, float ___1_delayTime, int32_t ___2_repeatCount, const RuntimeMethod* method) 
{
	{
		// mCallbackTick = 0;
		__this->___mCallbackTick_3 = 0;
		// mCallback = callback;
		Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* L_0 = ___0_callback;
		__this->___mCallback_2 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mCallback_2), (void*)L_0);
		// mDelayTime = delayTime;
		float L_1 = ___1_delayTime;
		__this->___mDelayTime_0 = L_1;
		// mRepeatCount = repeatCount;
		int32_t L_2 = ___2_repeatCount;
		__this->___mRepeatCount_1 = L_2;
		// }
		return;
	}
}
// System.Void QFramework.TimeItem::OnTimeTick()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem_OnTimeTick_m44B75346F9E639D11F9BEE52A282E6958CCDC759 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	int32_t V_0 = 0;
	{
		// if (mCallback != null)
		Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* L_0 = __this->___mCallback_2;
		if (!L_0)
		{
			goto IL_0024;
		}
	}
	{
		// mCallback(++mCallbackTick);
		Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* L_1 = __this->___mCallback_2;
		int32_t L_2 = __this->___mCallbackTick_3;
		V_0 = ((int32_t)il2cpp_codegen_add(L_2, 1));
		int32_t L_3 = V_0;
		__this->___mCallbackTick_3 = L_3;
		int32_t L_4 = V_0;
		NullCheck(L_1);
		Action_1_Invoke_mAC3C34BA1905AB5B79E483CD9BB082B7D667F703_inline(L_1, L_4, NULL);
	}

IL_0024:
	{
		// if (mRepeatCount > 0)
		int32_t L_5 = __this->___mRepeatCount_1;
		if ((((int32_t)L_5) <= ((int32_t)0)))
		{
			goto IL_003b;
		}
	}
	{
		// --mRepeatCount;
		int32_t L_6 = __this->___mRepeatCount_1;
		__this->___mRepeatCount_1 = ((int32_t)il2cpp_codegen_subtract(L_6, 1));
	}

IL_003b:
	{
		// }
		return;
	}
}
// System.Single QFramework.TimeItem::get_SortScore()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float TimeItem_get_SortScore_m77DBD8ADA7598C757D444823787D70A7FB5F18CF (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	{
		// public float SortScore { get; set; }
		float L_0 = __this->___U3CSortScoreU3Ek__BackingField_4;
		return L_0;
	}
}
// System.Void QFramework.TimeItem::set_SortScore(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem_set_SortScore_m3DA91B88557E9F0BC96DE66FC31183D0CB6FE265 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, float ___0_value, const RuntimeMethod* method) 
{
	{
		// public float SortScore { get; set; }
		float L_0 = ___0_value;
		__this->___U3CSortScoreU3Ek__BackingField_4 = L_0;
		return;
	}
}
// System.Int32 QFramework.TimeItem::get_HeapIndex()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR int32_t TimeItem_get_HeapIndex_m9E4DA1ACFDF34BEC76F623D2DC6CC35CF6AD1BA4 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	{
		// public int HeapIndex { get; set; }
		int32_t L_0 = __this->___U3CHeapIndexU3Ek__BackingField_5;
		return L_0;
	}
}
// System.Void QFramework.TimeItem::set_HeapIndex(System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem_set_HeapIndex_mDBFF7F50CE32C2EBA90226B46F7297D6B15E3EDD (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	{
		// public int HeapIndex { get; set; }
		int32_t L_0 = ___0_value;
		__this->___U3CHeapIndexU3Ek__BackingField_5 = L_0;
		return;
	}
}
// System.Boolean QFramework.TimeItem::get_IsEnable()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TimeItem_get_IsEnable_m1E801E518495A9332F23BCC999918EF4E7F13A20 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsEnable { get; private set; } = true;
		bool L_0 = __this->___U3CIsEnableU3Ek__BackingField_6;
		return L_0;
	}
}
// System.Void QFramework.TimeItem::set_IsEnable(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem_set_IsEnable_m30F9D438BD5927F3B6CFBB9254EAF38AA6666834 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		// public bool IsEnable { get; private set; } = true;
		bool L_0 = ___0_value;
		__this->___U3CIsEnableU3Ek__BackingField_6 = L_0;
		return;
	}
}
// System.Boolean QFramework.TimeItem::get_IsRecycled()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TimeItem_get_IsRecycled_m0E4F482CEE1CDCA13DAFC3AF49E46F6CF491ECB9 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsRecycled { get; set; }
		bool L_0 = __this->___U3CIsRecycledU3Ek__BackingField_7;
		return L_0;
	}
}
// System.Void QFramework.TimeItem::set_IsRecycled(System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem_set_IsRecycled_m0BD48250864E212D0521002B036E6AED41562964 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		// public bool IsRecycled { get; set; }
		bool L_0 = ___0_value;
		__this->___U3CIsRecycledU3Ek__BackingField_7 = L_0;
		return;
	}
}
// System.Void QFramework.TimeItem::Cancel()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem_Cancel_m37972167491A71932F518C5FECA2F73EE17CB62B (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	{
		// if (IsEnable)
		bool L_0;
		L_0 = TimeItem_get_IsEnable_m1E801E518495A9332F23BCC999918EF4E7F13A20_inline(__this, NULL);
		if (!L_0)
		{
			goto IL_0016;
		}
	}
	{
		// IsEnable = false;
		TimeItem_set_IsEnable_m30F9D438BD5927F3B6CFBB9254EAF38AA6666834_inline(__this, (bool)0, NULL);
		// mCallback = null;
		__this->___mCallback_2 = (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mCallback_2), (void*)(Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404*)NULL);
	}

IL_0016:
	{
		// }
		return;
	}
}
// System.Boolean QFramework.TimeItem::NeedRepeat()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool TimeItem_NeedRepeat_m53BFA90935C2F8AFE75AF8DC360B65B9CF798CB1 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	{
		// if (mRepeatCount == 0)
		int32_t L_0 = __this->___mRepeatCount_1;
		if (L_0)
		{
			goto IL_000a;
		}
	}
	{
		// return false;
		return (bool)0;
	}

IL_000a:
	{
		// return true;
		return (bool)1;
	}
}
// System.Single QFramework.TimeItem::DelayTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float TimeItem_DelayTime_m11860F52A7926A17AFB44D07A92618B709889A3E (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	{
		// return mDelayTime;
		float L_0 = __this->___mDelayTime_0;
		return L_0;
	}
}
// System.Void QFramework.TimeItem::OnRecycled()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem_OnRecycled_m6843FFD1014338E58B0C48E6EDD2D3F8C97394EC (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	{
		// mCallbackTick = 0;
		__this->___mCallbackTick_3 = 0;
		// mCallback = null;
		__this->___mCallback_2 = (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mCallback_2), (void*)(Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404*)NULL);
		// IsEnable = true;
		TimeItem_set_IsEnable_m30F9D438BD5927F3B6CFBB9254EAF38AA6666834_inline(__this, (bool)1, NULL);
		// HeapIndex = 0;
		TimeItem_set_HeapIndex_mDBFF7F50CE32C2EBA90226B46F7297D6B15E3EDD_inline(__this, 0, NULL);
		// }
		return;
	}
}
// System.Void QFramework.TimeItem::Recycle2Cache()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem_Recycle2Cache_mDC21B7F37537C5C7265A9112DBF0A7F70B04D578 (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&SafeObjectPool_1_get_Instance_m27DE656011C461C719BEA3A05D455044BCA2757A_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// SafeObjectPool<TimeItem>.Instance.Recycle(this);
		SafeObjectPool_1_tB9A3A3095B574142CF0139B7323BA66C80DCC342* L_0;
		L_0 = SafeObjectPool_1_get_Instance_m27DE656011C461C719BEA3A05D455044BCA2757A(SafeObjectPool_1_get_Instance_m27DE656011C461C719BEA3A05D455044BCA2757A_RuntimeMethod_var);
		NullCheck(L_0);
		bool L_1;
		L_1 = VirtualFuncInvoker1< bool, TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* >::Invoke(7 /* System.Boolean QFramework.Pool`1<QFramework.TimeItem>::Recycle(T) */, L_0, __this);
		// }
		return;
	}
}
// System.Void QFramework.TimeItem::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimeItem__ctor_m300DF9635D1243F5D9E123D23B47D353C1BFF22B (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsEnable { get; private set; } = true;
		__this->___U3CIsEnableU3Ek__BackingField_6 = (bool)1;
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
// QFramework.Timer QFramework.Timer::get_Instance()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* Timer_get_Instance_m20E57C957705CF1168B025EEAB98FAF69631694D (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&MonoSingletonProperty_1_get_Instance_m9EE6F4D4D2E389D777F50092E629D09BE4E90C76_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static Timer Instance => MonoSingletonProperty<Timer>.Instance;
		Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* L_0;
		L_0 = MonoSingletonProperty_1_get_Instance_m9EE6F4D4D2E389D777F50092E629D09BE4E90C76(MonoSingletonProperty_1_get_Instance_m9EE6F4D4D2E389D777F50092E629D09BE4E90C76_RuntimeMethod_var);
		return L_0;
	}
}
// System.Single QFramework.Timer::get_CurrentScaleTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Timer_get_CurrentScaleTime_m7BA42A1AC216700628563A32EAF29F8571ADF3AA (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, const RuntimeMethod* method) 
{
	{
		// public float CurrentScaleTime { get; private set; } = -1;
		float L_0 = __this->___U3CCurrentScaleTimeU3Ek__BackingField_7;
		return L_0;
	}
}
// System.Void QFramework.Timer::set_CurrentScaleTime(System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_set_CurrentScaleTime_m2429134D1ABDFB8CAAA020E62CD699B7598B7F2C (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, float ___0_value, const RuntimeMethod* method) 
{
	{
		// public float CurrentScaleTime { get; private set; } = -1;
		float L_0 = ___0_value;
		__this->___U3CCurrentScaleTimeU3Ek__BackingField_7 = L_0;
		return;
	}
}
// System.Void QFramework.Timer::OnSingletonInit()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_OnSingletonInit_m56E07835980CDF45451330DDD4AB5CE700001BEE (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BinaryHeap_1_Clear_mA0971C02BA613AC8B838BE7C9A470FD9D1F13EDB_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// mUnScaleTimeHeap.Clear();
		BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* L_0 = __this->___mUnScaleTimeHeap_4;
		NullCheck(L_0);
		BinaryHeap_1_Clear_mA0971C02BA613AC8B838BE7C9A470FD9D1F13EDB(L_0, BinaryHeap_1_Clear_mA0971C02BA613AC8B838BE7C9A470FD9D1F13EDB_RuntimeMethod_var);
		// mScaleTimeHeap.Clear();
		BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* L_1 = __this->___mScaleTimeHeap_5;
		NullCheck(L_1);
		BinaryHeap_1_Clear_mA0971C02BA613AC8B838BE7C9A470FD9D1F13EDB(L_1, BinaryHeap_1_Clear_mA0971C02BA613AC8B838BE7C9A470FD9D1F13EDB_RuntimeMethod_var);
		// mCurrentUnScaleTime = Time.unscaledTime;
		float L_2;
		L_2 = Time_get_unscaledTime_mAF4040B858903E1325D1C65B8BF1AC61460B2503(NULL);
		__this->___mCurrentUnScaleTime_6 = L_2;
		// CurrentScaleTime = Time.time;
		float L_3;
		L_3 = Time_get_time_m3A271BB1B20041144AC5B7863B71AB1F0150374B(NULL);
		Timer_set_CurrentScaleTime_m2429134D1ABDFB8CAAA020E62CD699B7598B7F2C_inline(__this, L_3, NULL);
		// }
		return;
	}
}
// QFramework.TimeItem QFramework.Timer::Post2Scale(System.Action`1<System.Int32>,System.Single,System.Int32)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* Timer_Post2Scale_m58F7D7517DF78356B41793A5FF4F930306E0117E (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* ___0_callback, float ___1_delay, int32_t ___2_repeat, const RuntimeMethod* method) 
{
	TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* V_0 = NULL;
	{
		// TimeItem item = TimeItem.Allocate(callback, delay, repeat);
		Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* L_0 = ___0_callback;
		float L_1 = ___1_delay;
		int32_t L_2 = ___2_repeat;
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_3;
		L_3 = TimeItem_Allocate_m8D72EDD32571CF80232A79522386B079D75B30D3(L_0, L_1, L_2, NULL);
		V_0 = L_3;
		// Post2Scale(item);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_4 = V_0;
		Timer_Post2Scale_m6022E05EE6077C187236232544C3CFC8A601513A(__this, L_4, NULL);
		// return item;
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_5 = V_0;
		return L_5;
	}
}
// QFramework.TimeItem QFramework.Timer::Post2Scale(System.Action`1<System.Int32>,System.Single)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* Timer_Post2Scale_m61CE9C095888EC19B9CB9B07CF2B81B446960B32 (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* ___0_callback, float ___1_delay, const RuntimeMethod* method) 
{
	TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* V_0 = NULL;
	{
		// TimeItem item = TimeItem.Allocate(callback, delay);
		Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* L_0 = ___0_callback;
		float L_1 = ___1_delay;
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_2;
		L_2 = TimeItem_Allocate_m8D72EDD32571CF80232A79522386B079D75B30D3(L_0, L_1, 1, NULL);
		V_0 = L_2;
		// Post2Scale(item);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_3 = V_0;
		Timer_Post2Scale_m6022E05EE6077C187236232544C3CFC8A601513A(__this, L_3, NULL);
		// return item;
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_4 = V_0;
		return L_4;
	}
}
// System.Void QFramework.Timer::Post2Scale(QFramework.TimeItem)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_Post2Scale_m6022E05EE6077C187236232544C3CFC8A601513A (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* ___0_item, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BinaryHeap_1_Insert_m0CE4B4F1F7097434EF0F96D95DD16C1592A72C8C_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// item.SortScore = CurrentScaleTime + item.DelayTime();
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_0 = ___0_item;
		float L_1;
		L_1 = Timer_get_CurrentScaleTime_m7BA42A1AC216700628563A32EAF29F8571ADF3AA_inline(__this, NULL);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_2 = ___0_item;
		NullCheck(L_2);
		float L_3;
		L_3 = TimeItem_DelayTime_m11860F52A7926A17AFB44D07A92618B709889A3E_inline(L_2, NULL);
		NullCheck(L_0);
		TimeItem_set_SortScore_m3DA91B88557E9F0BC96DE66FC31183D0CB6FE265_inline(L_0, ((float)il2cpp_codegen_add(L_1, L_3)), NULL);
		// mScaleTimeHeap.Insert(item);
		BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* L_4 = __this->___mScaleTimeHeap_5;
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_5 = ___0_item;
		NullCheck(L_4);
		BinaryHeap_1_Insert_m0CE4B4F1F7097434EF0F96D95DD16C1592A72C8C(L_4, L_5, BinaryHeap_1_Insert_m0CE4B4F1F7097434EF0F96D95DD16C1592A72C8C_RuntimeMethod_var);
		// }
		return;
	}
}
// System.Void QFramework.Timer::Post2Really(QFramework.TimeItem)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_Post2Really_m7E718D8C05E9572FFE752EA28903911BC8263943 (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* ___0_item, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BinaryHeap_1_Insert_m0CE4B4F1F7097434EF0F96D95DD16C1592A72C8C_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// item.SortScore = mCurrentUnScaleTime + item.DelayTime();
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_0 = ___0_item;
		float L_1 = __this->___mCurrentUnScaleTime_6;
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_2 = ___0_item;
		NullCheck(L_2);
		float L_3;
		L_3 = TimeItem_DelayTime_m11860F52A7926A17AFB44D07A92618B709889A3E_inline(L_2, NULL);
		NullCheck(L_0);
		TimeItem_set_SortScore_m3DA91B88557E9F0BC96DE66FC31183D0CB6FE265_inline(L_0, ((float)il2cpp_codegen_add(L_1, L_3)), NULL);
		// mUnScaleTimeHeap.Insert(item);
		BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* L_4 = __this->___mUnScaleTimeHeap_4;
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_5 = ___0_item;
		NullCheck(L_4);
		BinaryHeap_1_Insert_m0CE4B4F1F7097434EF0F96D95DD16C1592A72C8C(L_4, L_5, BinaryHeap_1_Insert_m0CE4B4F1F7097434EF0F96D95DD16C1592A72C8C_RuntimeMethod_var);
		// }
		return;
	}
}
// System.Void QFramework.Timer::Update()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_Update_mCFE90F85E536A33C63008CF22A30F01BE9686471 (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, const RuntimeMethod* method) 
{
	{
		// UpdateMgr();
		Timer_UpdateMgr_mD44545C2811DD9428327A15EE6EA23930C1FF770(__this, NULL);
		// }
		return;
	}
}
// System.Void QFramework.Timer::UpdateMgr()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_UpdateMgr_mD44545C2811DD9428327A15EE6EA23930C1FF770 (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BinaryHeap_1_Pop_mB0A91FAA9BBB3881F4743F07A49AC36C7B6796E1_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BinaryHeap_1_Top_m031CA8B0E52BBAD5A48F2732884CF6CC27C325E5_RuntimeMethod_var);
		s_Il2CppMethodInitialized = true;
	}
	TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* V_0 = NULL;
	{
		// TimeItem item = null;
		V_0 = (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8*)NULL;
		// mCurrentUnScaleTime = Time.unscaledTime;
		float L_0;
		L_0 = Time_get_unscaledTime_mAF4040B858903E1325D1C65B8BF1AC61460B2503(NULL);
		__this->___mCurrentUnScaleTime_6 = L_0;
		// CurrentScaleTime = Time.time;
		float L_1;
		L_1 = Time_get_time_m3A271BB1B20041144AC5B7863B71AB1F0150374B(NULL);
		Timer_set_CurrentScaleTime_m2429134D1ABDFB8CAAA020E62CD699B7598B7F2C_inline(__this, L_1, NULL);
		goto IL_0078;
	}

IL_001a:
	{
		// if (!item.IsEnable)
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_2 = V_0;
		NullCheck(L_2);
		bool L_3;
		L_3 = TimeItem_get_IsEnable_m1E801E518495A9332F23BCC999918EF4E7F13A20_inline(L_2, NULL);
		if (L_3)
		{
			goto IL_0036;
		}
	}
	{
		// mUnScaleTimeHeap.Pop();
		BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* L_4 = __this->___mUnScaleTimeHeap_4;
		NullCheck(L_4);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_5;
		L_5 = BinaryHeap_1_Pop_mB0A91FAA9BBB3881F4743F07A49AC36C7B6796E1(L_4, BinaryHeap_1_Pop_mB0A91FAA9BBB3881F4743F07A49AC36C7B6796E1_RuntimeMethod_var);
		// item.Recycle2Cache();
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_6 = V_0;
		NullCheck(L_6);
		TimeItem_Recycle2Cache_mDC21B7F37537C5C7265A9112DBF0A7F70B04D578(L_6, NULL);
		// continue;
		goto IL_0078;
	}

IL_0036:
	{
		// if (item.SortScore < mCurrentUnScaleTime)
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_7 = V_0;
		NullCheck(L_7);
		float L_8;
		L_8 = TimeItem_get_SortScore_m77DBD8ADA7598C757D444823787D70A7FB5F18CF_inline(L_7, NULL);
		float L_9 = __this->___mCurrentUnScaleTime_6;
		if ((!(((float)L_8) < ((float)L_9))))
		{
			goto IL_00e4;
		}
	}
	{
		// mUnScaleTimeHeap.Pop();
		BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* L_10 = __this->___mUnScaleTimeHeap_4;
		NullCheck(L_10);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_11;
		L_11 = BinaryHeap_1_Pop_mB0A91FAA9BBB3881F4743F07A49AC36C7B6796E1(L_10, BinaryHeap_1_Pop_mB0A91FAA9BBB3881F4743F07A49AC36C7B6796E1_RuntimeMethod_var);
		// item.OnTimeTick();
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_12 = V_0;
		NullCheck(L_12);
		TimeItem_OnTimeTick_m44B75346F9E639D11F9BEE52A282E6958CCDC759(L_12, NULL);
		// if (item.IsEnable && item.NeedRepeat())
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_13 = V_0;
		NullCheck(L_13);
		bool L_14;
		L_14 = TimeItem_get_IsEnable_m1E801E518495A9332F23BCC999918EF4E7F13A20_inline(L_13, NULL);
		if (!L_14)
		{
			goto IL_0072;
		}
	}
	{
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_15 = V_0;
		NullCheck(L_15);
		bool L_16;
		L_16 = TimeItem_NeedRepeat_m53BFA90935C2F8AFE75AF8DC360B65B9CF798CB1(L_15, NULL);
		if (!L_16)
		{
			goto IL_0072;
		}
	}
	{
		// Post2Really(item);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_17 = V_0;
		Timer_Post2Really_m7E718D8C05E9572FFE752EA28903911BC8263943(__this, L_17, NULL);
		goto IL_0078;
	}

IL_0072:
	{
		// item.Recycle2Cache();
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_18 = V_0;
		NullCheck(L_18);
		TimeItem_Recycle2Cache_mDC21B7F37537C5C7265A9112DBF0A7F70B04D578(L_18, NULL);
	}

IL_0078:
	{
		// while ((item = mUnScaleTimeHeap.Top()) != null)
		BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* L_19 = __this->___mUnScaleTimeHeap_4;
		NullCheck(L_19);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_20;
		L_20 = BinaryHeap_1_Top_m031CA8B0E52BBAD5A48F2732884CF6CC27C325E5(L_19, BinaryHeap_1_Top_m031CA8B0E52BBAD5A48F2732884CF6CC27C325E5_RuntimeMethod_var);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_21 = L_20;
		V_0 = L_21;
		if (L_21)
		{
			goto IL_001a;
		}
	}
	{
		goto IL_00e4;
	}

IL_0089:
	{
		// if (!item.IsEnable)
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_22 = V_0;
		NullCheck(L_22);
		bool L_23;
		L_23 = TimeItem_get_IsEnable_m1E801E518495A9332F23BCC999918EF4E7F13A20_inline(L_22, NULL);
		if (L_23)
		{
			goto IL_00a5;
		}
	}
	{
		// mScaleTimeHeap.Pop();
		BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* L_24 = __this->___mScaleTimeHeap_5;
		NullCheck(L_24);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_25;
		L_25 = BinaryHeap_1_Pop_mB0A91FAA9BBB3881F4743F07A49AC36C7B6796E1(L_24, BinaryHeap_1_Pop_mB0A91FAA9BBB3881F4743F07A49AC36C7B6796E1_RuntimeMethod_var);
		// item.Recycle2Cache();
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_26 = V_0;
		NullCheck(L_26);
		TimeItem_Recycle2Cache_mDC21B7F37537C5C7265A9112DBF0A7F70B04D578(L_26, NULL);
		// continue;
		goto IL_00e4;
	}

IL_00a5:
	{
		// if (item.SortScore < CurrentScaleTime)
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_27 = V_0;
		NullCheck(L_27);
		float L_28;
		L_28 = TimeItem_get_SortScore_m77DBD8ADA7598C757D444823787D70A7FB5F18CF_inline(L_27, NULL);
		float L_29;
		L_29 = Timer_get_CurrentScaleTime_m7BA42A1AC216700628563A32EAF29F8571ADF3AA_inline(__this, NULL);
		if ((!(((float)L_28) < ((float)L_29))))
		{
			goto IL_00f3;
		}
	}
	{
		// mScaleTimeHeap.Pop();
		BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* L_30 = __this->___mScaleTimeHeap_5;
		NullCheck(L_30);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_31;
		L_31 = BinaryHeap_1_Pop_mB0A91FAA9BBB3881F4743F07A49AC36C7B6796E1(L_30, BinaryHeap_1_Pop_mB0A91FAA9BBB3881F4743F07A49AC36C7B6796E1_RuntimeMethod_var);
		// item.OnTimeTick();
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_32 = V_0;
		NullCheck(L_32);
		TimeItem_OnTimeTick_m44B75346F9E639D11F9BEE52A282E6958CCDC759(L_32, NULL);
		// if (item.IsEnable && item.NeedRepeat())
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_33 = V_0;
		NullCheck(L_33);
		bool L_34;
		L_34 = TimeItem_get_IsEnable_m1E801E518495A9332F23BCC999918EF4E7F13A20_inline(L_33, NULL);
		if (!L_34)
		{
			goto IL_00de;
		}
	}
	{
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_35 = V_0;
		NullCheck(L_35);
		bool L_36;
		L_36 = TimeItem_NeedRepeat_m53BFA90935C2F8AFE75AF8DC360B65B9CF798CB1(L_35, NULL);
		if (!L_36)
		{
			goto IL_00de;
		}
	}
	{
		// Post2Scale(item);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_37 = V_0;
		Timer_Post2Scale_m6022E05EE6077C187236232544C3CFC8A601513A(__this, L_37, NULL);
		goto IL_00e4;
	}

IL_00de:
	{
		// item.Recycle2Cache();
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_38 = V_0;
		NullCheck(L_38);
		TimeItem_Recycle2Cache_mDC21B7F37537C5C7265A9112DBF0A7F70B04D578(L_38, NULL);
	}

IL_00e4:
	{
		// while ((item = mScaleTimeHeap.Top()) != null)
		BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* L_39 = __this->___mScaleTimeHeap_5;
		NullCheck(L_39);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_40;
		L_40 = BinaryHeap_1_Top_m031CA8B0E52BBAD5A48F2732884CF6CC27C325E5(L_39, BinaryHeap_1_Top_m031CA8B0E52BBAD5A48F2732884CF6CC27C325E5_RuntimeMethod_var);
		TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* L_41 = L_40;
		V_0 = L_41;
		if (L_41)
		{
			goto IL_0089;
		}
	}

IL_00f3:
	{
		// }
		return;
	}
}
// System.Void QFramework.Timer::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer__ctor_m60C2E3BA99E4400B712DCBDA2DF41605E28F1366 (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BinaryHeap_1__ctor_m3D6509830FB73EDAE5EAC9D72DF322E21B196B82_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// private readonly BinaryHeap<TimeItem> mUnScaleTimeHeap = new BinaryHeap<TimeItem>(128, BinaryHeapSortMode.kMin);
		BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* L_0 = (BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03*)il2cpp_codegen_object_new(BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		BinaryHeap_1__ctor_m3D6509830FB73EDAE5EAC9D72DF322E21B196B82(L_0, ((int32_t)128), 0, BinaryHeap_1__ctor_m3D6509830FB73EDAE5EAC9D72DF322E21B196B82_RuntimeMethod_var);
		__this->___mUnScaleTimeHeap_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mUnScaleTimeHeap_4), (void*)L_0);
		// private readonly BinaryHeap<TimeItem> mScaleTimeHeap = new BinaryHeap<TimeItem>(128, BinaryHeapSortMode.kMin);
		BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03* L_1 = (BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03*)il2cpp_codegen_object_new(BinaryHeap_1_t2AAB55C9D7D2B2034399B669E9293AC85288EE03_il2cpp_TypeInfo_var);
		NullCheck(L_1);
		BinaryHeap_1__ctor_m3D6509830FB73EDAE5EAC9D72DF322E21B196B82(L_1, ((int32_t)128), 0, BinaryHeap_1__ctor_m3D6509830FB73EDAE5EAC9D72DF322E21B196B82_RuntimeMethod_var);
		__this->___mScaleTimeHeap_5 = L_1;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___mScaleTimeHeap_5), (void*)L_1);
		// private float mCurrentUnScaleTime = -1;
		__this->___mCurrentUnScaleTime_6 = (-1.0f);
		// public float CurrentScaleTime { get; private set; } = -1;
		__this->___U3CCurrentScaleTimeU3Ek__BackingField_7 = (-1.0f);
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
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
// System.Single QFramework.TimeExtend.Timer::get_CurrentTime()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR float Timer_get_CurrentTime_m3B77F914DC06F585676CF36ADB0E4CCD63E0A2F0 (Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* __this, const RuntimeMethod* method) 
{
	{
		// private float CurrentTime => mIgnorTimescale ? Time.realtimeSinceStartup : UnityEngine.Time.time;
		bool L_0 = __this->___mIgnorTimescale_5;
		if (L_0)
		{
			goto IL_000e;
		}
	}
	{
		float L_1;
		L_1 = Time_get_time_m3A271BB1B20041144AC5B7863B71AB1F0150374B(NULL);
		return L_1;
	}

IL_000e:
	{
		float L_2;
		L_2 = Time_get_realtimeSinceStartup_m73B3CB73175D79A44333D59BB70F9EDE55EC9510(NULL);
		return L_2;
	}
}
// System.Void QFramework.TimeExtend.Timer::.ctor(System.Single,System.String,System.Boolean,System.Boolean)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer__ctor_mC7120A417B6416300CC7F327889C6B5BDE165C15 (Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* __this, float ___0_time, String_t* ___1_flag, bool ___2_loop, bool ___3_ignorTimescale, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Exists_mA5D007E602F98F4B2E9D7C5954BBAB42E8CB7888_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Predicate_1_t55DAAC67F486E666A2115F381B43C27ADF0B01CF_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass15_0_U3C_ctorU3Eb__0_m9BFF9FD45188C78680ED3AE443D19FA49875CB5A_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral52DAB56457890BF2D137A9A4E1BA2346E92DB450);
		s_Il2CppMethodInitialized = true;
	}
	U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA* V_0 = NULL;
	int32_t V_1 = 0;
	Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* G_B6_0 = NULL;
	Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* G_B5_0 = NULL;
	String_t* G_B7_0 = NULL;
	Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* G_B7_1 = NULL;
	{
		// private float mTime = -1;
		__this->___mTime_3 = (-1.0f);
		U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA* L_0 = (U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA*)il2cpp_codegen_object_new(U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		U3CU3Ec__DisplayClass15_0__ctor_mE7A02AE6DDC1583EB58402C7C6FEB12DCDFEA7DB(L_0, NULL);
		V_0 = L_0;
		U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA* L_1 = V_0;
		String_t* L_2 = ___1_flag;
		NullCheck(L_1);
		L_1->___flag_0 = L_2;
		Il2CppCodeGenWriteBarrier((void**)(&L_1->___flag_0), (void*)L_2);
		// private Timer(float time, string flag, bool loop = false, bool ignorTimescale = true)
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		// if (null == Driver) Driver = TimerDriver.Get; //???Time??
		il2cpp_codegen_runtime_class_init_inline(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* L_3 = ((Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields*)il2cpp_codegen_static_fields_for(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var))->___Driver_7;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_4;
		L_4 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605((Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, L_3, NULL);
		if (!L_4)
		{
			goto IL_0035;
		}
	}
	{
		// if (null == Driver) Driver = TimerDriver.Get; //???Time??
		TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* L_5;
		L_5 = TimerDriver_get_Get_m7485C3E16D307FB5ABAA222C83BF457B640BE373(NULL);
		il2cpp_codegen_runtime_class_init_inline(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		((Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields*)il2cpp_codegen_static_fields_for(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var))->___Driver_7 = L_5;
		Il2CppCodeGenWriteBarrier((void**)(&((Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields*)il2cpp_codegen_static_fields_for(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var))->___Driver_7), (void*)L_5);
	}

IL_0035:
	{
		// mTime = time;
		float L_6 = ___0_time;
		__this->___mTime_3 = L_6;
		// mLoop = loop;
		bool L_7 = ___2_loop;
		__this->___mLoop_4 = L_7;
		// mIgnorTimescale = ignorTimescale;
		bool L_8 = ___3_ignorTimescale;
		__this->___mIgnorTimescale_5 = L_8;
		// mCachedTime = CurrentTime;
		float L_9;
		L_9 = Timer_get_CurrentTime_m3B77F914DC06F585676CF36ADB0E4CCD63E0A2F0(__this, NULL);
		__this->___mCachedTime_8 = L_9;
		// if (timers.Exists((v) => { return v.mFlag == flag; }))
		il2cpp_codegen_runtime_class_init_inline(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* L_10 = ((Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields*)il2cpp_codegen_static_fields_for(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var))->___timers_0;
		U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA* L_11 = V_0;
		Predicate_1_t55DAAC67F486E666A2115F381B43C27ADF0B01CF* L_12 = (Predicate_1_t55DAAC67F486E666A2115F381B43C27ADF0B01CF*)il2cpp_codegen_object_new(Predicate_1_t55DAAC67F486E666A2115F381B43C27ADF0B01CF_il2cpp_TypeInfo_var);
		NullCheck(L_12);
		Predicate_1__ctor_mCF5C1738FF21D8836CA0F3017AD87E26CC807A92(L_12, L_11, (intptr_t)((void*)U3CU3Ec__DisplayClass15_0_U3C_ctorU3Eb__0_m9BFF9FD45188C78680ED3AE443D19FA49875CB5A_RuntimeMethod_var), NULL);
		NullCheck(L_10);
		bool L_13;
		L_13 = List_1_Exists_mA5D007E602F98F4B2E9D7C5954BBAB42E8CB7888(L_10, L_12, List_1_Exists_mA5D007E602F98F4B2E9D7C5954BBAB42E8CB7888_RuntimeMethod_var);
		if (!L_13)
		{
			goto IL_0088;
		}
	}
	{
		// if (ShowLog) Debug.LogWarningFormat("?TimerTrigger?????:?????????{0}??", flag);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_14 = (ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918*)SZArrayNew(ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918_il2cpp_TypeInfo_var, (uint32_t)1);
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_15 = L_14;
		U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA* L_16 = V_0;
		NullCheck(L_16);
		String_t* L_17 = L_16->___flag_0;
		NullCheck(L_15);
		ArrayElementTypeCheck (L_15, L_17);
		(L_15)->SetAt(static_cast<il2cpp_array_size_t>(0), (RuntimeObject*)L_17);
		il2cpp_codegen_runtime_class_init_inline(Debug_t8394C7EEAECA3689C2C9B9DE9C7166D73596276F_il2cpp_TypeInfo_var);
		Debug_LogWarningFormat_mD8224DEBCB6050F4E2BF55151F0C6A29B87DEFBC(_stringLiteral52DAB56457890BF2D137A9A4E1BA2346E92DB450, L_15, NULL);
	}

IL_0088:
	{
		// mFlag = string.IsNullOrEmpty(flag) ? GetHashCode().ToString() : flag;//???????
		U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA* L_18 = V_0;
		NullCheck(L_18);
		String_t* L_19 = L_18->___flag_0;
		bool L_20;
		L_20 = String_IsNullOrEmpty_mEA9E3FB005AC28FE02E69FCF95A7B8456192B478(L_19, NULL);
		G_B5_0 = __this;
		if (L_20)
		{
			G_B6_0 = __this;
			goto IL_009e;
		}
	}
	{
		U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA* L_21 = V_0;
		NullCheck(L_21);
		String_t* L_22 = L_21->___flag_0;
		G_B7_0 = L_22;
		G_B7_1 = G_B5_0;
		goto IL_00ac;
	}

IL_009e:
	{
		int32_t L_23;
		L_23 = VirtualFuncInvoker0< int32_t >::Invoke(2 /* System.Int32 System.Object::GetHashCode() */, __this);
		V_1 = L_23;
		String_t* L_24;
		L_24 = Int32_ToString_m030E01C24E294D6762FB0B6F37CB541581F55CA5((&V_1), NULL);
		G_B7_0 = L_24;
		G_B7_1 = G_B6_0;
	}

IL_00ac:
	{
		NullCheck(G_B7_1);
		G_B7_1->___mFlag_6 = G_B7_0;
		Il2CppCodeGenWriteBarrier((void**)(&G_B7_1->___mFlag_6), (void*)G_B7_0);
		// }
		return;
	}
}
// System.Void QFramework.TimeExtend.Timer::Update()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_Update_m71FC7B00FA77E5DC52D6F584B63946752B70DDCE (Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* __this, const RuntimeMethod* method) 
{
	{
		// if (!mIsFinish && !mIsPause) //???
		bool L_0 = __this->___mIsFinish_10;
		if (L_0)
		{
			goto IL_0084;
		}
	}
	{
		bool L_1 = __this->___mIsPause_11;
		if (L_1)
		{
			goto IL_0084;
		}
	}
	{
		// mTimePassed = CurrentTime - mCachedTime;
		float L_2;
		L_2 = Timer_get_CurrentTime_m3B77F914DC06F585676CF36ADB0E4CCD63E0A2F0(__this, NULL);
		float L_3 = __this->___mCachedTime_8;
		__this->___mTimePassed_9 = ((float)il2cpp_codegen_subtract(L_2, L_3));
		// if (null != UpdateEvent) UpdateEvent.Invoke(Mathf.Clamp01(mTimePassed / mTime));
		Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* L_4 = __this->___UpdateEvent_1;
		if (!L_4)
		{
			goto IL_0048;
		}
	}
	{
		// if (null != UpdateEvent) UpdateEvent.Invoke(Mathf.Clamp01(mTimePassed / mTime));
		Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* L_5 = __this->___UpdateEvent_1;
		float L_6 = __this->___mTimePassed_9;
		float L_7 = __this->___mTime_3;
		float L_8;
		L_8 = Mathf_Clamp01_mA7E048DBDA832D399A581BE4D6DED9FA44CE0F14_inline(((float)(L_6/L_7)), NULL);
		NullCheck(L_5);
		Action_1_Invoke_mA8F89FB04FEA0F48A4F22EC84B5F9ADB2914341F_inline(L_5, L_8, NULL);
	}

IL_0048:
	{
		// if (mTimePassed >= mTime)
		float L_9 = __this->___mTimePassed_9;
		float L_10 = __this->___mTime_3;
		if ((!(((float)L_9) >= ((float)L_10))))
		{
			goto IL_0084;
		}
	}
	{
		// if (null != EndEvent) EndEvent.Invoke();
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_11 = __this->___EndEvent_2;
		if (!L_11)
		{
			goto IL_0069;
		}
	}
	{
		// if (null != EndEvent) EndEvent.Invoke();
		Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* L_12 = __this->___EndEvent_2;
		NullCheck(L_12);
		Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline(L_12, NULL);
	}

IL_0069:
	{
		// if (mLoop)
		bool L_13 = __this->___mLoop_4;
		if (!L_13)
		{
			goto IL_007e;
		}
	}
	{
		// mCachedTime = CurrentTime;
		float L_14;
		L_14 = Timer_get_CurrentTime_m3B77F914DC06F585676CF36ADB0E4CCD63E0A2F0(__this, NULL);
		__this->___mCachedTime_8 = L_14;
		return;
	}

IL_007e:
	{
		// Stop();
		Timer_Stop_mDD7E5C13871F2A59DD2258C783C8F6FB2846C120(__this, NULL);
	}

IL_0084:
	{
		// }
		return;
	}
}
// System.Void QFramework.TimeExtend.Timer::Stop()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_Stop_mDD7E5C13871F2A59DD2258C783C8F6FB2846C120 (Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Contains_m35660B24F32E8D50C67E6AE681D8E2084B5D8276_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_Remove_mE6E4B96921EE86B995707B091ABCBEEC1E894C22_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// if (timers.Contains(this))
		il2cpp_codegen_runtime_class_init_inline(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* L_0 = ((Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields*)il2cpp_codegen_static_fields_for(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var))->___timers_0;
		NullCheck(L_0);
		bool L_1;
		L_1 = List_1_Contains_m35660B24F32E8D50C67E6AE681D8E2084B5D8276(L_0, __this, List_1_Contains_m35660B24F32E8D50C67E6AE681D8E2084B5D8276_RuntimeMethod_var);
		if (!L_1)
		{
			goto IL_0019;
		}
	}
	{
		// timers.Remove(this);
		il2cpp_codegen_runtime_class_init_inline(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* L_2 = ((Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields*)il2cpp_codegen_static_fields_for(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var))->___timers_0;
		NullCheck(L_2);
		bool L_3;
		L_3 = List_1_Remove_mE6E4B96921EE86B995707B091ABCBEEC1E894C22(L_2, __this, List_1_Remove_mE6E4B96921EE86B995707B091ABCBEEC1E894C22_RuntimeMethod_var);
	}

IL_0019:
	{
		// mTime = -1;
		__this->___mTime_3 = (-1.0f);
		// mIsFinish = true;
		__this->___mIsFinish_10 = (bool)1;
		// mIsPause = false;
		__this->___mIsPause_11 = (bool)0;
		// UpdateEvent = null;
		__this->___UpdateEvent_1 = (Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___UpdateEvent_1), (void*)(Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A*)NULL);
		// EndEvent = null;
		__this->___EndEvent_2 = (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___EndEvent_2), (void*)(Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07*)NULL);
		// }
		return;
	}
}
// System.Void QFramework.TimeExtend.Timer::UpdateAllTimer()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer_UpdateAllTimer_m20568CB7C2E1B96B17452777B184DEAB1A94976D (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Count_mA7DA63FD079A8939EE109E799F9A48EB03198C96_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_get_Item_m593E51A37B3107E675E10D79C053FB4EE0857080_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	int32_t V_0 = 0;
	{
		// for (int i = 0; i < timers.Count; i++)
		V_0 = 0;
		goto IL_0025;
	}

IL_0004:
	{
		// if (null != timers[i])
		il2cpp_codegen_runtime_class_init_inline(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* L_0 = ((Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields*)il2cpp_codegen_static_fields_for(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var))->___timers_0;
		int32_t L_1 = V_0;
		NullCheck(L_0);
		Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* L_2;
		L_2 = List_1_get_Item_m593E51A37B3107E675E10D79C053FB4EE0857080(L_0, L_1, List_1_get_Item_m593E51A37B3107E675E10D79C053FB4EE0857080_RuntimeMethod_var);
		if (!L_2)
		{
			goto IL_0021;
		}
	}
	{
		// timers[i].Update();
		il2cpp_codegen_runtime_class_init_inline(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* L_3 = ((Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields*)il2cpp_codegen_static_fields_for(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var))->___timers_0;
		int32_t L_4 = V_0;
		NullCheck(L_3);
		Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* L_5;
		L_5 = List_1_get_Item_m593E51A37B3107E675E10D79C053FB4EE0857080(L_3, L_4, List_1_get_Item_m593E51A37B3107E675E10D79C053FB4EE0857080_RuntimeMethod_var);
		NullCheck(L_5);
		Timer_Update_m71FC7B00FA77E5DC52D6F584B63946752B70DDCE(L_5, NULL);
	}

IL_0021:
	{
		// for (int i = 0; i < timers.Count; i++)
		int32_t L_6 = V_0;
		V_0 = ((int32_t)il2cpp_codegen_add(L_6, 1));
	}

IL_0025:
	{
		// for (int i = 0; i < timers.Count; i++)
		int32_t L_7 = V_0;
		il2cpp_codegen_runtime_class_init_inline(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* L_8 = ((Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields*)il2cpp_codegen_static_fields_for(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var))->___timers_0;
		NullCheck(L_8);
		int32_t L_9;
		L_9 = List_1_get_Count_mA7DA63FD079A8939EE109E799F9A48EB03198C96_inline(L_8, List_1_get_Count_mA7DA63FD079A8939EE109E799F9A48EB03198C96_RuntimeMethod_var);
		if ((((int32_t)L_7) < ((int32_t)L_9)))
		{
			goto IL_0004;
		}
	}
	{
		// }
		return;
	}
}
// System.Void QFramework.TimeExtend.Timer::.cctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void Timer__cctor_mD9C389A5EFAAE813FC2582AFA0743F3368231C62 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1__ctor_m137F537B4FA09945E22CF1C0390E1A538D3D8F2C_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// static List<Timer> timers = new List<Timer>();
		List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2* L_0 = (List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2*)il2cpp_codegen_object_new(List_1_tA2B2572528F3DEEA5BD1C647CD877FF7080893F2_il2cpp_TypeInfo_var);
		NullCheck(L_0);
		List_1__ctor_m137F537B4FA09945E22CF1C0390E1A538D3D8F2C(L_0, List_1__ctor_m137F537B4FA09945E22CF1C0390E1A538D3D8F2C_RuntimeMethod_var);
		((Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields*)il2cpp_codegen_static_fields_for(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var))->___timers_0 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&((Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields*)il2cpp_codegen_static_fields_for(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var))->___timers_0), (void*)L_0);
		// public static TimerDriver Driver = null;//?????????????????
		((Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields*)il2cpp_codegen_static_fields_for(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var))->___Driver_7 = (TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2*)NULL;
		Il2CppCodeGenWriteBarrier((void**)(&((Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_StaticFields*)il2cpp_codegen_static_fields_for(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var))->___Driver_7), (void*)(TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2*)NULL);
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
// System.Void QFramework.TimeExtend.Timer/<>c__DisplayClass15_0::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void U3CU3Ec__DisplayClass15_0__ctor_mE7A02AE6DDC1583EB58402C7C6FEB12DCDFEA7DB (U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA* __this, const RuntimeMethod* method) 
{
	{
		Object__ctor_mE837C6B9FA8C6D5D109F4B2EC885D79919AC0EA2(__this, NULL);
		return;
	}
}
// System.Boolean QFramework.TimeExtend.Timer/<>c__DisplayClass15_0::<.ctor>b__0(QFramework.TimeExtend.Timer)
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR bool U3CU3Ec__DisplayClass15_0_U3C_ctorU3Eb__0_m9BFF9FD45188C78680ED3AE443D19FA49875CB5A (U3CU3Ec__DisplayClass15_0_t535FA99EB95D97C21EF00C35ABD4A4169EF012DA* __this, Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* ___0_v, const RuntimeMethod* method) 
{
	{
		// if (timers.Exists((v) => { return v.mFlag == flag; }))
		Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4* L_0 = ___0_v;
		NullCheck(L_0);
		String_t* L_1 = L_0->___mFlag_6;
		String_t* L_2 = __this->___flag_0;
		bool L_3;
		L_3 = String_op_Equality_m030E1B219352228970A076136E455C4E568C02C1(L_1, L_2, NULL);
		return L_3;
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
// QFramework.TimeExtend.TimerDriver QFramework.TimeExtend.TimerDriver::get_Get()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* TimerDriver_get_Get_m7485C3E16D307FB5ABAA222C83BF457B640BE373 (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_AddComponent_TisTimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_m6E1162BC6BB854F682DC3C069CCE84DCC39A8411_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&GameObject_t76FEDD663AB33C991A9C9A23129337651094216F_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_FindObjectOfType_TisTimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_mA4C61CEE004298FBD022CDE9621561524631EE2E_RuntimeMethod_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_il2cpp_TypeInfo_var);
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&_stringLiteral13B62B289497AADBE4E98E91944A655E327A7ABD);
		s_Il2CppMethodInitialized = true;
	}
	TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* G_B3_0 = NULL;
	TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* G_B2_0 = NULL;
	{
		// if (null == mInstance)
		TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* L_0 = ((TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_StaticFields*)il2cpp_codegen_static_fields_for(TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_il2cpp_TypeInfo_var))->___mInstance_4;
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		bool L_1;
		L_1 = Object_op_Equality_mB6120F782D83091EF56A198FCEBCF066DB4A9605((Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C*)NULL, L_0, NULL);
		if (!L_1)
		{
			goto IL_002a;
		}
	}
	{
		// mInstance = FindObjectOfType<TimerDriver>() ?? new GameObject("TimerEntity").AddComponent<TimerDriver>();
		il2cpp_codegen_runtime_class_init_inline(Object_tC12DECB6760A7F2CBF65D9DCF18D044C2D97152C_il2cpp_TypeInfo_var);
		TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* L_2;
		L_2 = Object_FindObjectOfType_TisTimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_mA4C61CEE004298FBD022CDE9621561524631EE2E(Object_FindObjectOfType_TisTimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_mA4C61CEE004298FBD022CDE9621561524631EE2E_RuntimeMethod_var);
		TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* L_3 = L_2;
		G_B2_0 = L_3;
		if (L_3)
		{
			G_B3_0 = L_3;
			goto IL_0025;
		}
	}
	{
		GameObject_t76FEDD663AB33C991A9C9A23129337651094216F* L_4 = (GameObject_t76FEDD663AB33C991A9C9A23129337651094216F*)il2cpp_codegen_object_new(GameObject_t76FEDD663AB33C991A9C9A23129337651094216F_il2cpp_TypeInfo_var);
		NullCheck(L_4);
		GameObject__ctor_m37D512B05D292F954792225E6C6EEE95293A9B88(L_4, _stringLiteral13B62B289497AADBE4E98E91944A655E327A7ABD, NULL);
		NullCheck(L_4);
		TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* L_5;
		L_5 = GameObject_AddComponent_TisTimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_m6E1162BC6BB854F682DC3C069CCE84DCC39A8411(L_4, GameObject_AddComponent_TisTimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_m6E1162BC6BB854F682DC3C069CCE84DCC39A8411_RuntimeMethod_var);
		G_B3_0 = L_5;
	}

IL_0025:
	{
		((TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_StaticFields*)il2cpp_codegen_static_fields_for(TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_il2cpp_TypeInfo_var))->___mInstance_4 = G_B3_0;
		Il2CppCodeGenWriteBarrier((void**)(&((TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_StaticFields*)il2cpp_codegen_static_fields_for(TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_il2cpp_TypeInfo_var))->___mInstance_4), (void*)G_B3_0);
	}

IL_002a:
	{
		// return mInstance;
		TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* L_6 = ((TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_StaticFields*)il2cpp_codegen_static_fields_for(TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_il2cpp_TypeInfo_var))->___mInstance_4;
		return L_6;
	}
}
// System.Void QFramework.TimeExtend.TimerDriver::Awake()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimerDriver_Awake_m6D64904B37419B7B81CD874295841BB43D3B3297 (TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// mInstance = this;
		((TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_StaticFields*)il2cpp_codegen_static_fields_for(TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_il2cpp_TypeInfo_var))->___mInstance_4 = __this;
		Il2CppCodeGenWriteBarrier((void**)(&((TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_StaticFields*)il2cpp_codegen_static_fields_for(TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2_il2cpp_TypeInfo_var))->___mInstance_4), (void*)__this);
		// }
		return;
	}
}
// System.Void QFramework.TimeExtend.TimerDriver::Update()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimerDriver_Update_m46DCB38B67B98CB2DB39383486E5FC689AC24777 (TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* __this, const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// Timer.UpdateAllTimer();
		il2cpp_codegen_runtime_class_init_inline(Timer_t0A7BCC8C5CE62572AB982FB8751586E6F5DB36D4_il2cpp_TypeInfo_var);
		Timer_UpdateAllTimer_m20568CB7C2E1B96B17452777B184DEAB1A94976D(NULL);
		// }
		return;
	}
}
// System.Void QFramework.TimeExtend.TimerDriver::.ctor()
IL2CPP_EXTERN_C IL2CPP_METHOD_ATTR void TimerDriver__ctor_m64A0FB0FA50F61B2DE358D45682A2891C35828A1 (TimerDriver_t3EA268155478A0EE6A2E3182B84B8B1C816FBEB2* __this, const RuntimeMethod* method) 
{
	{
		MonoBehaviour__ctor_m592DB0105CA0BC97AA1C5F4AD27B12D68A3B7C1E(__this, NULL);
		return;
	}
}
#ifdef __clang__
#pragma clang diagnostic pop
#endif
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_Invoke_m7126A54DACA72B845424072887B5F3A51FC3808E_inline (Action_tD00B0A84D7945E50C2DFFC28EFEE6ED44ED2AD07* __this, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioManager_get_MusicPlayer_m8046BC707549A3B916F8C074D12CB7E6C79E27BA_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) 
{
	{
		// public AudioPlayer MusicPlayer { get; private set; }
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_0 = __this->___U3CMusicPlayerU3Ek__BackingField_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioManager_set_CurrentMusicName_mE05C1458C0BCB217A49C8ABD589B4D4A8E77087C_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		// public string CurrentMusicName { get; set; }
		String_t* L_0 = ___0_value;
		__this->___U3CCurrentMusicNameU3Ek__BackingField_8 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CCurrentMusicNameU3Ek__BackingField_8), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* AudioManager_get_VoicePlayer_m2FFBB17585A0BEEB38C67CF63A693A9F38C87D3D_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) 
{
	{
		// public AudioPlayer VoicePlayer { get; private set; }
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_0 = __this->___U3CVoicePlayerU3Ek__BackingField_5;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioManager_set_CurrentVoiceName_m8203991273B51FA9A0FD8E9361C2D512E73CB6E4_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, String_t* ___0_value, const RuntimeMethod* method) 
{
	{
		// public string CurrentVoiceName { get; set; }
		String_t* L_0 = ___0_value;
		__this->___U3CCurrentVoiceNameU3Ek__BackingField_9 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CCurrentVoiceNameU3Ek__BackingField_9), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* AudioKit_get_Settings_mF6D1EE1C01000826B713287019037BA2AA60CE20_inline (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	{
		// public static AudioKitSettings Settings { get; } = new AudioKitSettings();
		il2cpp_codegen_runtime_class_init_inline(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var);
		AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* L_0 = ((AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_StaticFields*)il2cpp_codegen_static_fields_for(AudioKit_t4DD8D8E90590225EFA8C77D010160C36FB5B2FA2_il2cpp_TypeInfo_var))->___U3CSettingsU3Ek__BackingField_6;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* AudioKitSettings_get_IsVoiceOn_m40198BDFF0BAC22A00FF9A7D42A90D9AF6BDF937_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsBooleanProperty IsVoiceOn { get; private set; }
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0 = __this->___U3CIsVoiceOnU3Ek__BackingField_8;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* AudioKitSettings_get_IsSoundOn_m16E5D82CD5CE9BDAA868820A75FA970239276A85_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsBooleanProperty IsSoundOn { get; private set; }
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0 = __this->___U3CIsSoundOnU3Ek__BackingField_6;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* AudioKitSettings_get_SoundVolume_mD628B2C76B4EE61E1BF923E41126A63B0F3B897A_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsFloatProperty SoundVolume { get; private set; }
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_0 = __this->___U3CSoundVolumeU3Ek__BackingField_9;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* AudioManager_get_CurrentMusicName_m46E7270EA32A4B0CD9B4261AF7CB78A75C6444FB_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) 
{
	{
		// public string CurrentMusicName { get; set; }
		String_t* L_0 = __this->___U3CCurrentMusicNameU3Ek__BackingField_8;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* AudioManager_get_CurrentVoiceName_m2A6026179BDD9F9E07A287B6935786E0CCF24FA4_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, const RuntimeMethod* method) 
{
	{
		// public string CurrentVoiceName { get; set; }
		String_t* L_0 = __this->___U3CCurrentVoiceNameU3Ek__BackingField_9;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_IsSoundOn_m3B8938CB24A93E9864A4152064F6A7E66B12FD22_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* ___0_value, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsBooleanProperty IsSoundOn { get; private set; }
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0 = ___0_value;
		__this->___U3CIsSoundOnU3Ek__BackingField_6 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CIsSoundOnU3Ek__BackingField_6), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_IsMusicOn_mB7BB81A6F1F2CF2A58C28A0FF40783B6603D48CB_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* ___0_value, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsBooleanProperty IsMusicOn { get; private set; }
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0 = ___0_value;
		__this->___U3CIsMusicOnU3Ek__BackingField_7 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CIsMusicOnU3Ek__BackingField_7), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_IsVoiceOn_m22A05BA843579B70656A6B4B7C95163882DD30C0_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* ___0_value, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsBooleanProperty IsVoiceOn { get; private set; }
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0 = ___0_value;
		__this->___U3CIsVoiceOnU3Ek__BackingField_8 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CIsVoiceOnU3Ek__BackingField_8), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_IsOn_m4E88FB457E5A9FA95CAB7591BD86C9ECC5A7742E_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A* ___0_value, const RuntimeMethod* method) 
{
	{
		// public CustomProperty<bool> IsOn { get; private set; }
		CustomProperty_1_tA27AB09AF4DD5A96F25477A0426A8BC45A5F194A* L_0 = ___0_value;
		__this->___U3CIsOnU3Ek__BackingField_12 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CIsOnU3Ek__BackingField_12), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_SoundVolume_mAB196708DD249054162A3B89B53CD1C5564A9954_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* ___0_value, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsFloatProperty SoundVolume { get; private set; }
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_0 = ___0_value;
		__this->___U3CSoundVolumeU3Ek__BackingField_9 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CSoundVolumeU3Ek__BackingField_9), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_MusicVolume_m31F575D40B044BC109FE19B8AEF8575C8995A55B_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* ___0_value, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsFloatProperty MusicVolume { get; private set; }
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_0 = ___0_value;
		__this->___U3CMusicVolumeU3Ek__BackingField_10 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CMusicVolumeU3Ek__BackingField_10), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioKitSettings_set_VoiceVolume_m9477C782964C3652242B54F32C82906A44350D3A_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* ___0_value, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsFloatProperty VoiceVolume { get; private set; }
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_0 = ___0_value;
		__this->___U3CVoiceVolumeU3Ek__BackingField_11 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CVoiceVolumeU3Ek__BackingField_11), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* AudioKitSettings_get_IsMusicOn_mB0034F4E51AEAC7FF364B848A6A42CD30E1CBB89_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsBooleanProperty IsMusicOn { get; private set; }
		PlayerPrefsBooleanProperty_t71C109D9E3CF1096CAD32153A6A28CFC328B11E3* L_0 = __this->___U3CIsMusicOnU3Ek__BackingField_7;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* AudioKitSettings_get_MusicVolume_mC12F412255E5C5D80823C6B401EA3B76CEA9EC84_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsFloatProperty MusicVolume { get; private set; }
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_0 = __this->___U3CMusicVolumeU3Ek__BackingField_10;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioManager_set_MusicPlayer_m19CF9C6DCBACE60B3B532458F590BE8B4A6B9ACF_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_value, const RuntimeMethod* method) 
{
	{
		// public AudioPlayer MusicPlayer { get; private set; }
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_0 = ___0_value;
		__this->___U3CMusicPlayerU3Ek__BackingField_4 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CMusicPlayerU3Ek__BackingField_4), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioPlayer_set_UsedCache_m0D255C3A9BF12259B211DFCF4C155AE5EC2A2DEA_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		// public bool UsedCache { get; set; } = true;
		bool L_0 = ___0_value;
		__this->___U3CUsedCacheU3Ek__BackingField_12 = L_0;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioPlayer_set_IsLoop_mB1AC04B836A3D80C92450B55197BDDFF6463AC17_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		// public bool IsLoop { get; set; }
		bool L_0 = ___0_value;
		__this->___U3CIsLoopU3Ek__BackingField_14 = L_0;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* AudioKitSettings_get_VoiceVolume_m67A62365F356F9A35A31A385452EDBCBFB99F3C1_inline (AudioKitSettings_tAEFB101F6E494186D4F1F568FFA2A62F5B8037B8* __this, const RuntimeMethod* method) 
{
	{
		// public PlayerPrefsFloatProperty VoiceVolume { get; private set; }
		PlayerPrefsFloatProperty_tD189F33E35FE452AD18F5776F85A05990AD35793* L_0 = __this->___U3CVoiceVolumeU3Ek__BackingField_11;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioManager_set_VoicePlayer_mE9427043D0CD26480779BA21020D4C22FD7E40BC_inline (AudioManager_t70C5444596B9A6D4A289F2D15F8AB3F264BF3274* __this, AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* ___0_value, const RuntimeMethod* method) 
{
	{
		// public AudioPlayer VoicePlayer { get; private set; }
		AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* L_0 = ___0_value;
		__this->___U3CVoicePlayerU3Ek__BackingField_5 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CVoicePlayerU3Ek__BackingField_5), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 Vector3_get_zero_m0C1249C3F25B1C70EAD3CC8B31259975A457AE39_inline (const RuntimeMethod* method) 
{
	static bool s_Il2CppMethodInitialized;
	if (!s_Il2CppMethodInitialized)
	{
		il2cpp_codegen_initialize_runtime_metadata((uintptr_t*)&Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var);
		s_Il2CppMethodInitialized = true;
	}
	Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 V_0;
	memset((&V_0), 0, sizeof(V_0));
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_0 = ((Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_StaticFields*)il2cpp_codegen_static_fields_for(Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2_il2cpp_TypeInfo_var))->___zeroVector_5;
		V_0 = L_0;
		goto IL_0009;
	}

IL_0009:
	{
		Vector3_t24C512C7B96BBABAD472002D0BA2BDA40A5A80B2 L_1 = V_0;
		return L_1;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR String_t* AudioPlayer_get_GetName_mCD8D1164E27DDF8F85DD7DF840EF0E4C77340060_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// public string GetName => mName;
		String_t* L_0 = __this->___mName_2;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void AudioPlayer_set_Volume_m8D35738EFB189D11C6FBFA3A1C116B3C4D007F5E_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* ___0_value, const RuntimeMethod* method) 
{
	{
		// public BindableProperty<float> Volume { get; set; }
		BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* L_0 = ___0_value;
		__this->___U3CVolumeU3Ek__BackingField_3 = L_0;
		Il2CppCodeGenWriteBarrier((void**)(&__this->___U3CVolumeU3Ek__BackingField_3), (void*)L_0);
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* AudioPlayer_get_Volume_m2095A2461F76393AA590905BEB51E7E90A174126_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// public BindableProperty<float> Volume { get; set; }
		BindableProperty_1_t922B777C025F558836D5A12A03B970A7CDB0EF58* L_0 = __this->___U3CVolumeU3Ek__BackingField_3;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float TimeItem_get_SortScore_m77DBD8ADA7598C757D444823787D70A7FB5F18CF_inline (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	{
		// public float SortScore { get; set; }
		float L_0 = __this->___U3CSortScoreU3Ek__BackingField_4;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Timer_get_CurrentScaleTime_m7BA42A1AC216700628563A32EAF29F8571ADF3AA_inline (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, const RuntimeMethod* method) 
{
	{
		// public float CurrentScaleTime { get; private set; } = -1;
		float L_0 = __this->___U3CCurrentScaleTimeU3Ek__BackingField_7;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool AudioPlayer_get_IsLoop_mC43549D534C455179A16FF1C2EDA241B117717E7_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsLoop { get; set; }
		bool L_0 = __this->___U3CIsLoopU3Ek__BackingField_14;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool AudioPlayer_get_UsedCache_m90781A61EB4E15DE4B973135C4D973D661351885_inline (AudioPlayer_t9104610845E7C221E44D4D007129DA18012EFA59* __this, const RuntimeMethod* method) 
{
	{
		// public bool UsedCache { get; set; } = true;
		bool L_0 = __this->___U3CUsedCacheU3Ek__BackingField_12;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR bool TimeItem_get_IsEnable_m1E801E518495A9332F23BCC999918EF4E7F13A20_inline (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	{
		// public bool IsEnable { get; private set; } = true;
		bool L_0 = __this->___U3CIsEnableU3Ek__BackingField_6;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void TimeItem_set_IsEnable_m30F9D438BD5927F3B6CFBB9254EAF38AA6666834_inline (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, bool ___0_value, const RuntimeMethod* method) 
{
	{
		// public bool IsEnable { get; private set; } = true;
		bool L_0 = ___0_value;
		__this->___U3CIsEnableU3Ek__BackingField_6 = L_0;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void TimeItem_set_HeapIndex_mDBFF7F50CE32C2EBA90226B46F7297D6B15E3EDD_inline (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, int32_t ___0_value, const RuntimeMethod* method) 
{
	{
		// public int HeapIndex { get; set; }
		int32_t L_0 = ___0_value;
		__this->___U3CHeapIndexU3Ek__BackingField_5 = L_0;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Timer_set_CurrentScaleTime_m2429134D1ABDFB8CAAA020E62CD699B7598B7F2C_inline (Timer_tAF6AE06333BDD1A2CFD7F1033DB7BA05903B111D* __this, float ___0_value, const RuntimeMethod* method) 
{
	{
		// public float CurrentScaleTime { get; private set; } = -1;
		float L_0 = ___0_value;
		__this->___U3CCurrentScaleTimeU3Ek__BackingField_7 = L_0;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float TimeItem_DelayTime_m11860F52A7926A17AFB44D07A92618B709889A3E_inline (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, const RuntimeMethod* method) 
{
	{
		// return mDelayTime;
		float L_0 = __this->___mDelayTime_0;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void TimeItem_set_SortScore_m3DA91B88557E9F0BC96DE66FC31183D0CB6FE265_inline (TimeItem_tB78A478F5045F13FC0BDAF17459E1561D242C5B8* __this, float ___0_value, const RuntimeMethod* method) 
{
	{
		// public float SortScore { get; set; }
		float L_0 = ___0_value;
		__this->___U3CSortScoreU3Ek__BackingField_4 = L_0;
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR float Mathf_Clamp01_mA7E048DBDA832D399A581BE4D6DED9FA44CE0F14_inline (float ___0_value, const RuntimeMethod* method) 
{
	bool V_0 = false;
	float V_1 = 0.0f;
	bool V_2 = false;
	{
		float L_0 = ___0_value;
		V_0 = (bool)((((float)L_0) < ((float)(0.0f)))? 1 : 0);
		bool L_1 = V_0;
		if (!L_1)
		{
			goto IL_0015;
		}
	}
	{
		V_1 = (0.0f);
		goto IL_002d;
	}

IL_0015:
	{
		float L_2 = ___0_value;
		V_2 = (bool)((((float)L_2) > ((float)(1.0f)))? 1 : 0);
		bool L_3 = V_2;
		if (!L_3)
		{
			goto IL_0029;
		}
	}
	{
		V_1 = (1.0f);
		goto IL_002d;
	}

IL_0029:
	{
		float L_4 = ___0_value;
		V_1 = L_4;
		goto IL_002d;
	}

IL_002d:
	{
		float L_5 = V_1;
		return L_5;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mF2422B2DD29F74CE66F791C3F68E288EC7C3DB9E_gshared_inline (Action_1_t6F9EB113EB3F16226AEF811A2744F4111C116C87* __this, RuntimeObject* ___0_obj, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t Stack_1_get_Count_mD08AE71D49787D30DDD9D484BCD323D646744D2E_gshared_inline (Stack_1_tAD790A47551563636908E21E4F08C54C0C323EB5* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____size_1;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_2_Invoke_m5387D08742D6C89CAB31D981C0F63C08D70AB3AD_gshared_inline (Action_2_t4E94B0FCA1084D7868DB11A50767A4916CA3D3FB* __this, bool ___0_arg1, RuntimeObject* ___1_arg2, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, bool, RuntimeObject*, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___0_arg1, ___1_arg2, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void List_1_Add_mEBCF994CC3814631017F46A387B1A192ED6C85C7_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, RuntimeObject* ___0_item, const RuntimeMethod* method) 
{
	ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* V_0 = NULL;
	int32_t V_1 = 0;
	{
		int32_t L_0 = __this->____version_3;
		__this->____version_3 = ((int32_t)il2cpp_codegen_add(L_0, 1));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_1 = __this->____items_1;
		V_0 = L_1;
		int32_t L_2 = __this->____size_2;
		V_1 = L_2;
		int32_t L_3 = V_1;
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_4 = V_0;
		NullCheck(L_4);
		if ((!(((uint32_t)L_3) < ((uint32_t)((int32_t)(((RuntimeArray*)L_4)->max_length))))))
		{
			goto IL_0034;
		}
	}
	{
		int32_t L_5 = V_1;
		__this->____size_2 = ((int32_t)il2cpp_codegen_add(L_5, 1));
		ObjectU5BU5D_t8061030B0A12A55D5AD8652A20C922FE99450918* L_6 = V_0;
		int32_t L_7 = V_1;
		RuntimeObject* L_8 = ___0_item;
		NullCheck(L_6);
		(L_6)->SetAt(static_cast<il2cpp_array_size_t>(L_7), (RuntimeObject*)L_8);
		return;
	}

IL_0034:
	{
		RuntimeObject* L_9 = ___0_item;
		((  void (*) (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D*, RuntimeObject*, const RuntimeMethod*))il2cpp_codegen_get_method_pointer(il2cpp_rgctx_method(method->klass->rgctx_data, 11)))(__this, L_9, il2cpp_rgctx_method(method->klass->rgctx_data, 11));
		return;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR RuntimeObject* KeyValuePair_2_get_Value_mC6BD8075F9C9DDEF7B4D731E5C38EC19103988E7_gshared_inline (KeyValuePair_2_tFC32D2507216293851350D29B64D79F950B55230* __this, const RuntimeMethod* method) 
{
	{
		RuntimeObject* L_0 = __this->___value_1;
		return L_0;
	}
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mAC3C34BA1905AB5B79E483CD9BB082B7D667F703_gshared_inline (Action_1_tD69A6DC9FBE94131E52F5A73B2A9D4AB51EEC404* __this, int32_t ___0_obj, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, int32_t, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR void Action_1_Invoke_mA8F89FB04FEA0F48A4F22EC84B5F9ADB2914341F_gshared_inline (Action_1_t310F18CB4338A2740CA701F160C62E2C3198E66A* __this, float ___0_obj, const RuntimeMethod* method) 
{
	typedef void (*FunctionPointerType) (RuntimeObject*, float, const RuntimeMethod*);
	((FunctionPointerType)__this->___invoke_impl_1)((Il2CppObject*)__this->___method_code_6, ___0_obj, reinterpret_cast<RuntimeMethod*>(__this->___method_3));
}
IL2CPP_MANAGED_FORCE_INLINE IL2CPP_METHOD_ATTR int32_t List_1_get_Count_m4407E4C389F22B8CEC282C15D56516658746C383_gshared_inline (List_1_tA239CB83DE5615F348BB0507E45F490F4F7C9A8D* __this, const RuntimeMethod* method) 
{
	{
		int32_t L_0 = __this->____size_2;
		return L_0;
	}
}
